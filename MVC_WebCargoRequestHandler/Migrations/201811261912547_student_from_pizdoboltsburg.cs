namespace MVC_WebCargoRequestHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student_from_pizdoboltsburg : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CargoForms", "ReceiptDate", c => c.DateTime());
            AlterColumn("dbo.CargoForms", "ResponseDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CargoForms", "ResponseDate", c => c.String());
            AlterColumn("dbo.CargoForms", "ReceiptDate", c => c.String());
        }
    }
}
