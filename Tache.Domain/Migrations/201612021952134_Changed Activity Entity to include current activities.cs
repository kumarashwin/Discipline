namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedActivityEntitytoincludecurrentactivities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Start", c => c.DateTime());
            AddColumn("dbo.Activities", "Default", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Default");
            DropColumn("dbo.Activities", "Start");
        }
    }
}
