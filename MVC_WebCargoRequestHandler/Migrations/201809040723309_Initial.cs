namespace MVC_WebCargoRequestHandler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CargoForms",
                c => new
                    {
                        CargoFormID = c.Int(nullable: false, identity: true),
                        CommunicationID = c.Int(nullable: false),
                        ReceiptDate = c.String(),
                        Customer = c.String(),
                        Departure = c.String(),
                        Destination = c.String(),
                        CargoDescription = c.String(),
                        CargoCode = c.String(),
                        RollingStockID = c.Int(nullable: false),
                        Cost = c.String(),
                        ResponseDate = c.String(),
                        Note = c.String(),
                        Feedback = c.String(),
                        TrafficClassificationID = c.Int(nullable: false),
                        DirectionID = c.Int(nullable: false),
                        ResidencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CargoFormID)
                .ForeignKey("dbo.CommunicationMethods", t => t.CommunicationID, cascadeDelete: true)
                .ForeignKey("dbo.Directions", t => t.DirectionID, cascadeDelete: true)
                .ForeignKey("dbo.Residencies", t => t.ResidencyID, cascadeDelete: true)
                .ForeignKey("dbo.RollingStockTypes", t => t.RollingStockID, cascadeDelete: true)
                .ForeignKey("dbo.TrafficClassifications", t => t.TrafficClassificationID, cascadeDelete: true)
                .Index(t => t.CommunicationID)
                .Index(t => t.RollingStockID)
                .Index(t => t.TrafficClassificationID)
                .Index(t => t.DirectionID)
                .Index(t => t.ResidencyID);
            
            CreateTable(
                "dbo.CommunicationMethods",
                c => new
                    {
                        CommunicationID = c.Int(nullable: false, identity: true),
                        CommunicationName = c.String(),
                    })
                .PrimaryKey(t => t.CommunicationID);
            
            CreateTable(
                "dbo.Directions",
                c => new
                    {
                        DirectionID = c.Int(nullable: false, identity: true),
                        DirectionName = c.String(),
                    })
                .PrimaryKey(t => t.DirectionID);
            
            CreateTable(
                "dbo.Residencies",
                c => new
                    {
                        ResidencyID = c.Int(nullable: false, identity: true),
                        ResidencyName = c.String(),
                    })
                .PrimaryKey(t => t.ResidencyID);
            
            CreateTable(
                "dbo.RollingStockTypes",
                c => new
                    {
                        RollingStockID = c.Int(nullable: false, identity: true),
                        RollingStockName = c.String(),
                    })
                .PrimaryKey(t => t.RollingStockID);
            
            CreateTable(
                "dbo.TrafficClassifications",
                c => new
                    {
                        TrafficClassificationID = c.Int(nullable: false, identity: true),
                        TrafficClassificationName = c.String(),
                    })
                .PrimaryKey(t => t.TrafficClassificationID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CargoForms", "TrafficClassificationID", "dbo.TrafficClassifications");
            DropForeignKey("dbo.CargoForms", "RollingStockID", "dbo.RollingStockTypes");
            DropForeignKey("dbo.CargoForms", "ResidencyID", "dbo.Residencies");
            DropForeignKey("dbo.CargoForms", "DirectionID", "dbo.Directions");
            DropForeignKey("dbo.CargoForms", "CommunicationID", "dbo.CommunicationMethods");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CargoForms", new[] { "ResidencyID" });
            DropIndex("dbo.CargoForms", new[] { "DirectionID" });
            DropIndex("dbo.CargoForms", new[] { "TrafficClassificationID" });
            DropIndex("dbo.CargoForms", new[] { "RollingStockID" });
            DropIndex("dbo.CargoForms", new[] { "CommunicationID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.TrafficClassifications");
            DropTable("dbo.RollingStockTypes");
            DropTable("dbo.Residencies");
            DropTable("dbo.Directions");
            DropTable("dbo.CommunicationMethods");
            DropTable("dbo.CargoForms");
        }
    }
}
