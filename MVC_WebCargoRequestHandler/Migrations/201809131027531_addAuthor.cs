namespace MVC_WebCargoRequestHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CargoForms", "Author", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CargoForms", "Author");
        }
    }
}
