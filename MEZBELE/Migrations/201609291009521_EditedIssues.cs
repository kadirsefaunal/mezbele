namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedIssues : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Issues_Id", "dbo.Issues");
            DropForeignKey("dbo.Issues", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Issues", "Users_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Issues_Id" });
            DropIndex("dbo.Issues", new[] { "Creator_Id" });
            DropIndex("dbo.Issues", new[] { "Users_Id" });
            CreateTable(
                "dbo.IssuesUsers",
                c => new
                    {
                        Issues_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Issues_Id, t.Users_Id })
                .ForeignKey("dbo.Issues", t => t.Issues_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Issues_Id)
                .Index(t => t.Users_Id);
            
            AddColumn("dbo.Crews", "Name", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.Crews", "Avatar", c => c.String());
            AddColumn("dbo.Issues", "CreatorId", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Issues_Id");
            DropColumn("dbo.Crews", "CrewName");
            DropColumn("dbo.Crews", "CrewAvatar");
            DropColumn("dbo.Issues", "Creator_Id");
            DropColumn("dbo.Issues", "Users_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Issues", "Users_Id", c => c.Int());
            AddColumn("dbo.Issues", "Creator_Id", c => c.Int());
            AddColumn("dbo.Crews", "CrewAvatar", c => c.String());
            AddColumn("dbo.Crews", "CrewName", c => c.String(nullable: false, maxLength: 25));
            AddColumn("dbo.Users", "Issues_Id", c => c.Int());
            DropForeignKey("dbo.IssuesUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.IssuesUsers", "Issues_Id", "dbo.Issues");
            DropIndex("dbo.IssuesUsers", new[] { "Users_Id" });
            DropIndex("dbo.IssuesUsers", new[] { "Issues_Id" });
            DropColumn("dbo.Issues", "CreatorId");
            DropColumn("dbo.Crews", "Avatar");
            DropColumn("dbo.Crews", "Name");
            DropTable("dbo.IssuesUsers");
            CreateIndex("dbo.Issues", "Users_Id");
            CreateIndex("dbo.Issues", "Creator_Id");
            CreateIndex("dbo.Users", "Issues_Id");
            AddForeignKey("dbo.Issues", "Users_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Issues", "Creator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Issues_Id", "dbo.Issues", "Id");
        }
    }
}
