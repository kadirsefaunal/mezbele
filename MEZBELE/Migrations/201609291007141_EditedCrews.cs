namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedCrews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Crews", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Crews_Id", "dbo.Crews");
            DropForeignKey("dbo.Crews", "Users_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Crews_Id" });
            DropIndex("dbo.Crews", new[] { "Owner_Id" });
            DropIndex("dbo.Crews", new[] { "Users_Id" });
            CreateTable(
                "dbo.CrewsUsers",
                c => new
                    {
                        Crews_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Crews_Id, t.Users_Id })
                .ForeignKey("dbo.Crews", t => t.Crews_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Crews_Id)
                .Index(t => t.Users_Id);
            
            AddColumn("dbo.Crews", "OwnerId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Crews_Id");
            DropColumn("dbo.Crews", "Owner_Id");
            DropColumn("dbo.Crews", "Users_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Crews", "Users_Id", c => c.Int());
            AddColumn("dbo.Crews", "Owner_Id", c => c.Int());
            AddColumn("dbo.Users", "Crews_Id", c => c.Int());
            DropForeignKey("dbo.CrewsUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.CrewsUsers", "Crews_Id", "dbo.Crews");
            DropIndex("dbo.CrewsUsers", new[] { "Users_Id" });
            DropIndex("dbo.CrewsUsers", new[] { "Crews_Id" });
            DropColumn("dbo.Crews", "OwnerId");
            DropTable("dbo.CrewsUsers");
            CreateIndex("dbo.Crews", "Users_Id");
            CreateIndex("dbo.Crews", "Owner_Id");
            CreateIndex("dbo.Users", "Crews_Id");
            AddForeignKey("dbo.Crews", "Users_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Crews_Id", "dbo.Crews", "Id");
            AddForeignKey("dbo.Crews", "Owner_Id", "dbo.Users", "Id");
        }
    }
}
