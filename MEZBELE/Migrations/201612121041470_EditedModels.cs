namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Processes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProcessName = c.String(nullable: false, maxLength: 200),
                        StartingDate = c.DateTime(nullable: false),
                        DeadLine = c.DateTime(nullable: false),
                        ParentProcess_Id = c.Int(nullable: false),
                        Projects_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Processes", t => t.ParentProcess_Id)
                .ForeignKey("dbo.Projects", t => t.Projects_Id)
                .Index(t => t.ParentProcess_Id)
                .Index(t => t.Projects_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Users", "Role_Id", c => c.Int());
            AddColumn("dbo.Issues", "DeadLine", c => c.DateTime(nullable: false));
            AddColumn("dbo.Issues", "RelatedProcess_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Bolge", c => c.String(maxLength: 100));
            CreateIndex("dbo.Users", "Role_Id");
            CreateIndex("dbo.Issues", "RelatedProcess_Id");
            AddForeignKey("dbo.Issues", "RelatedProcess_Id", "dbo.Processes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Users", "Role_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Processes", "Projects_Id", "dbo.Projects");
            DropForeignKey("dbo.Processes", "ParentProcess_Id", "dbo.Processes");
            DropForeignKey("dbo.Issues", "RelatedProcess_Id", "dbo.Processes");
            DropIndex("dbo.Issues", new[] { "RelatedProcess_Id" });
            DropIndex("dbo.Processes", new[] { "Projects_Id" });
            DropIndex("dbo.Processes", new[] { "ParentProcess_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            AlterColumn("dbo.Users", "Bolge", c => c.String(maxLength: 50));
            DropColumn("dbo.Issues", "RelatedProcess_Id");
            DropColumn("dbo.Issues", "DeadLine");
            DropColumn("dbo.Users", "Role_Id");
            DropTable("dbo.Roles");
            DropTable("dbo.Processes");
        }
    }
}
