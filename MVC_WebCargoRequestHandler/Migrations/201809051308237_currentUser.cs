namespace MVC_WebCargoRequestHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currentUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CargoForms", "currentUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CargoForms", "currentUserId");
        }
    }
}
