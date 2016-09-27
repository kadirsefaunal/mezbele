namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedModelAttributes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UsersProjects", newName: "ProjectsUsers");
            DropForeignKey("dbo.UsersCrews", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.UsersCrews", "Crews_Id", "dbo.Crews");
            DropForeignKey("dbo.Comments", "IssuesId", "dbo.Issues");
            DropIndex("dbo.Comments", new[] { "IssuesId" });
            DropIndex("dbo.UsersCrews", new[] { "Users_Id" });
            DropIndex("dbo.UsersCrews", new[] { "Crews_Id" });
            RenameColumn(table: "dbo.Issues", name: "Projects_Id", newName: "Project_Id");
            RenameColumn(table: "dbo.Comments", name: "IssuesId", newName: "Issue_Id");
            RenameColumn(table: "dbo.Comments", name: "Users_Id", newName: "Commenter_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_Users_Id", newName: "IX_Commenter_Id");
            RenameIndex(table: "dbo.Issues", name: "IX_Projects_Id", newName: "IX_Project_Id");
            DropPrimaryKey("dbo.ProjectsUsers");
            AddColumn("dbo.Crews", "Owner_Id", c => c.Int());
            AddColumn("dbo.Crews", "Users_Id", c => c.Int());
            AddColumn("dbo.Projects", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Issues", "Creator_Id", c => c.Int());
            AddColumn("dbo.Users", "Crews_Id", c => c.Int());
            AlterColumn("dbo.Comments", "Issue_Id", c => c.Int());
            AlterColumn("dbo.Comments", "Comment", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Crews", "CrewName", c => c.String(nullable: false, maxLength: 25));
            AlterColumn("dbo.Projects", "Description", c => c.String(nullable: false, maxLength: 250));
            AlterColumn("dbo.Issues", "IssuesName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Issues", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 50));
            AlterColumn("dbo.Users", "EMail", c => c.String(maxLength: 250));
            AddPrimaryKey("dbo.ProjectsUsers", new[] { "Projects_Id", "Users_Id" });
            CreateIndex("dbo.Comments", "Issue_Id");
            CreateIndex("dbo.Users", "Crews_Id");
            CreateIndex("dbo.Crews", "Owner_Id");
            CreateIndex("dbo.Crews", "Users_Id");
            CreateIndex("dbo.Issues", "Creator_Id");
            AddForeignKey("dbo.Crews", "Owner_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Issues", "Creator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Crews_Id", "dbo.Crews", "Id");
            AddForeignKey("dbo.Crews", "Users_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Comments", "Issue_Id", "dbo.Issues", "Id");
            DropColumn("dbo.Comments", "UserId");
            DropColumn("dbo.Crews", "OwnerId");
            DropColumn("dbo.Projects", "ProjectName");
            DropColumn("dbo.Issues", "ProjectID");
            DropColumn("dbo.Issues", "Creator");
            DropTable("dbo.UsersCrews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersCrews",
                c => new
                    {
                        Users_Id = c.Int(nullable: false),
                        Crews_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_Id, t.Crews_Id });
            
            AddColumn("dbo.Issues", "Creator", c => c.Int(nullable: false));
            AddColumn("dbo.Issues", "ProjectID", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "ProjectName", c => c.String());
            AddColumn("dbo.Crews", "OwnerId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "Issue_Id", "dbo.Issues");
            DropForeignKey("dbo.Crews", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Crews_Id", "dbo.Crews");
            DropForeignKey("dbo.Issues", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Crews", "Owner_Id", "dbo.Users");
            DropIndex("dbo.Issues", new[] { "Creator_Id" });
            DropIndex("dbo.Crews", new[] { "Users_Id" });
            DropIndex("dbo.Crews", new[] { "Owner_Id" });
            DropIndex("dbo.Users", new[] { "Crews_Id" });
            DropIndex("dbo.Comments", new[] { "Issue_Id" });
            DropPrimaryKey("dbo.ProjectsUsers");
            AlterColumn("dbo.Users", "EMail", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Users", "Password", c => c.String());
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Issues", "Description", c => c.String());
            AlterColumn("dbo.Issues", "IssuesName", c => c.String());
            AlterColumn("dbo.Projects", "Description", c => c.String());
            AlterColumn("dbo.Crews", "CrewName", c => c.String());
            AlterColumn("dbo.Comments", "Comment", c => c.String());
            AlterColumn("dbo.Comments", "Issue_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Crews_Id");
            DropColumn("dbo.Issues", "Creator_Id");
            DropColumn("dbo.Projects", "Name");
            DropColumn("dbo.Crews", "Users_Id");
            DropColumn("dbo.Crews", "Owner_Id");
            AddPrimaryKey("dbo.ProjectsUsers", new[] { "Users_Id", "Projects_Id" });
            RenameIndex(table: "dbo.Issues", name: "IX_Project_Id", newName: "IX_Projects_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_Commenter_Id", newName: "IX_Users_Id");
            RenameColumn(table: "dbo.Comments", name: "Commenter_Id", newName: "Users_Id");
            RenameColumn(table: "dbo.Comments", name: "Issue_Id", newName: "IssuesId");
            RenameColumn(table: "dbo.Issues", name: "Project_Id", newName: "Projects_Id");
            CreateIndex("dbo.UsersCrews", "Crews_Id");
            CreateIndex("dbo.UsersCrews", "Users_Id");
            CreateIndex("dbo.Comments", "IssuesId");
            AddForeignKey("dbo.Comments", "IssuesId", "dbo.Issues", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersCrews", "Crews_Id", "dbo.Crews", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsersCrews", "Users_Id", "dbo.Users", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.ProjectsUsers", newName: "UsersProjects");
        }
    }
}
