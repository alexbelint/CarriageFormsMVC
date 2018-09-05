namespace MVC_WebCargoRequestHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReceiptDateAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CargoForms", "ReceiptDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CargoForms", "ReceiptDate");
        }
    }
}
