namespace WebAPI2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WebAPIDemoContestEnrollments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonId = c.Int(nullable: false),
                        ContestId = c.Int(nullable: false),
                        EnrolledOn = c.DateTime(nullable: false),
                        RespondedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebAPIDemoContests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GoLiveTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebAPIDemoSMS",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebAPIDemoRoles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.WebAPIDemoRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebAPIDemoSMS", "Role_Id", "dbo.WebAPIDemoRoles");
            DropIndex("dbo.WebAPIDemoSMS", new[] { "Role_Id" });
            DropTable("dbo.WebAPIDemoRoles");
            DropTable("dbo.WebAPIDemoSMS");
            DropTable("dbo.WebAPIDemoContests");
            DropTable("dbo.WebAPIDemoContestEnrollments");
        }
    }
}
