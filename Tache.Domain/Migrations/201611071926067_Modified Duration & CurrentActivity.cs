namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDurationCurrentActivity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CurrentActivities", "DurationId", "dbo.Durations");
            DropIndex("dbo.CurrentActivities", new[] { "DurationId" });
            AddColumn("dbo.CurrentActivities", "Start", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Durations", "To", c => c.DateTime(nullable: false));
            DropColumn("dbo.CurrentActivities", "DurationId");
            DropColumn("dbo.Durations", "TimeSpent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Durations", "TimeSpent", c => c.Long());
            AddColumn("dbo.CurrentActivities", "DurationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Durations", "To", c => c.DateTime());
            DropColumn("dbo.CurrentActivities", "Start");
            CreateIndex("dbo.CurrentActivities", "DurationId");
            AddForeignKey("dbo.CurrentActivities", "DurationId", "dbo.Durations", "Id", cascadeDelete: true);
        }
    }
}
