namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreationDate = c.DateTime(nullable: false),
                        Commenter_Id = c.Int(),
                        Issue_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Commenter_Id)
                .ForeignKey("dbo.Issues", t => t.Issue_Id)
                .Index(t => t.Commenter_Id)
                .Index(t => t.Issue_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        EMail = c.String(maxLength: 250),
                        UserAvatar = c.String(),
                        Issues_Id = c.Int(),
                        Crews_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Issues", t => t.Issues_Id)
                .ForeignKey("dbo.Crews", t => t.Crews_Id)
                .Index(t => t.Issues_Id)
                .Index(t => t.Crews_Id);
            
            CreateTable(
                "dbo.Crews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CrewName = c.String(nullable: false, maxLength: 25),
                        CrewAvatar = c.String(),
                        Owner_Id = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Owner_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Owner_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerID = c.Int(nullable: false),
                        IsIndividual = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 250),
                        CreationDate = c.DateTime(nullable: false),
                        ChangeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        IssuesName = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 500),
                        CreationDate = c.DateTime(nullable: false),
                        Creator_Id = c.Int(),
                        Project_Id = c.Int(),
                        Users_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Creator_Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .ForeignKey("dbo.Users", t => t.Users_Id)
                .Index(t => t.Creator_Id)
                .Index(t => t.Project_Id)
                .Index(t => t.Users_Id);
            
            CreateTable(
                "dbo.ProjectsCrews",
                c => new
                    {
                        Projects_Id = c.Int(nullable: false),
                        Crews_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Projects_Id, t.Crews_Id })
                .ForeignKey("dbo.Projects", t => t.Projects_Id, cascadeDelete: true)
                .ForeignKey("dbo.Crews", t => t.Crews_Id, cascadeDelete: true)
                .Index(t => t.Projects_Id)
                .Index(t => t.Crews_Id);
            
            CreateTable(
                "dbo.ProjectsUsers",
                c => new
                    {
                        Projects_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Projects_Id, t.Users_Id })
                .ForeignKey("dbo.Projects", t => t.Projects_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Projects_Id)
                .Index(t => t.Users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Issues", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Crews", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Crews_Id", "dbo.Crews");
            DropForeignKey("dbo.ProjectsUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.ProjectsUsers", "Projects_Id", "dbo.Projects");
            DropForeignKey("dbo.Issues", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Issues", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Issue_Id", "dbo.Issues");
            DropForeignKey("dbo.Users", "Issues_Id", "dbo.Issues");
            DropForeignKey("dbo.ProjectsCrews", "Crews_Id", "dbo.Crews");
            DropForeignKey("dbo.ProjectsCrews", "Projects_Id", "dbo.Projects");
            DropForeignKey("dbo.Crews", "Owner_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Commenter_Id", "dbo.Users");
            DropIndex("dbo.ProjectsUsers", new[] { "Users_Id" });
            DropIndex("dbo.ProjectsUsers", new[] { "Projects_Id" });
            DropIndex("dbo.ProjectsCrews", new[] { "Crews_Id" });
            DropIndex("dbo.ProjectsCrews", new[] { "Projects_Id" });
            DropIndex("dbo.Issues", new[] { "Users_Id" });
            DropIndex("dbo.Issues", new[] { "Project_Id" });
            DropIndex("dbo.Issues", new[] { "Creator_Id" });
            DropIndex("dbo.Crews", new[] { "Users_Id" });
            DropIndex("dbo.Crews", new[] { "Owner_Id" });
            DropIndex("dbo.Users", new[] { "Crews_Id" });
            DropIndex("dbo.Users", new[] { "Issues_Id" });
            DropIndex("dbo.Comments", new[] { "Issue_Id" });
            DropIndex("dbo.Comments", new[] { "Commenter_Id" });
            DropTable("dbo.ProjectsUsers");
            DropTable("dbo.ProjectsCrews");
            DropTable("dbo.Issues");
            DropTable("dbo.Projects");
            DropTable("dbo.Crews");
            DropTable("dbo.Users");
            DropTable("dbo.Comments");
        }
    }
}
