namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Budgets", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.CurrentActivities", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Budgets", new[] { "ActivityId" });
            DropIndex("dbo.CurrentActivities", new[] { "ActivityId" });
            AddColumn("dbo.Activities", "UserName", c => c.String(nullable: false));
            DropTable("dbo.Budgets");
            DropTable("dbo.CurrentActivities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CurrentActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        TimeInTicks = c.Long(nullable: false),
                        Period = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Activities", "UserName");
            CreateIndex("dbo.CurrentActivities", "ActivityId");
            CreateIndex("dbo.Budgets", "ActivityId");
            AddForeignKey("dbo.CurrentActivities", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Budgets", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
    }
}
