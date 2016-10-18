namespace MEZBELE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "WebAdresi", c => c.String(maxLength: 100));
            AddColumn("dbo.Users", "Konum", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "Bolge", c => c.String(maxLength: 50));
            AddColumn("dbo.Users", "SonGiris", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "SonGiris");
            DropColumn("dbo.Users", "Bolge");
            DropColumn("dbo.Users", "Konum");
            DropColumn("dbo.Users", "WebAdresi");
        }
    }
}
