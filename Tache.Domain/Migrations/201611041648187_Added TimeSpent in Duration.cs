namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeSpentinDuration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Durations", "TimeSpent", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Durations", "TimeSpent");
        }
    }
}
