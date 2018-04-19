namespace WebAPI2Demo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SMSContest1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebAPIDemoSMS", "ContestDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebAPIDemoSMS", "ContestDate");
        }
    }
}
