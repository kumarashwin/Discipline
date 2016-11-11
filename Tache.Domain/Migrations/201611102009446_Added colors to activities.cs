namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedcolorstoactivities : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Color");
        }
    }
}
