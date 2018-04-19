namespace WebAPI2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMSContest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebAPIDemoSMS", "WebAPIDemoContest_Id", c => c.Int());
            AlterColumn("dbo.WebAPIDemoContests", "GoLiveTime", c => c.DateTime());
            CreateIndex("dbo.WebAPIDemoSMS", "WebAPIDemoContest_Id");
            AddForeignKey("dbo.WebAPIDemoSMS", "WebAPIDemoContest_Id", "dbo.WebAPIDemoContests", "Id");
            DropTable("dbo.WebAPIDemoContestEnrollments");
        }
        
        public override void Down()
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
            
            DropForeignKey("dbo.WebAPIDemoSMS", "WebAPIDemoContest_Id", "dbo.WebAPIDemoContests");
            DropIndex("dbo.WebAPIDemoSMS", new[] { "WebAPIDemoContest_Id" });
            AlterColumn("dbo.WebAPIDemoContests", "GoLiveTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.WebAPIDemoSMS", "WebAPIDemoContest_Id");
        }
    }
}
