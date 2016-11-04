namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialDBSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Budgets", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.CurrentActivities", "Activity_Id", "dbo.Activities");
            DropForeignKey("dbo.CurrentActivities", "Duration_Id", "dbo.Durations");
            DropForeignKey("dbo.Durations", "Activity_Id", "dbo.Activities");
            DropIndex("dbo.Budgets", new[] { "Activity_Id" });
            DropIndex("dbo.CurrentActivities", new[] { "Activity_Id" });
            DropIndex("dbo.CurrentActivities", new[] { "Duration_Id" });
            DropIndex("dbo.Durations", new[] { "Activity_Id" });
            RenameColumn(table: "dbo.Budgets", name: "Activity_Id", newName: "ActivityId");
            RenameColumn(table: "dbo.CurrentActivities", name: "Activity_Id", newName: "ActivityId");
            RenameColumn(table: "dbo.CurrentActivities", name: "Duration_Id", newName: "DurationId");
            RenameColumn(table: "dbo.Durations", name: "Activity_Id", newName: "ActivityId");
            AlterColumn("dbo.Budgets", "ActivityId", c => c.Int(nullable: false));
            AlterColumn("dbo.CurrentActivities", "ActivityId", c => c.Int(nullable: false));
            AlterColumn("dbo.CurrentActivities", "DurationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Durations", "ActivityId", c => c.Int(nullable: false));
            CreateIndex("dbo.Budgets", "ActivityId");
            CreateIndex("dbo.CurrentActivities", "ActivityId");
            CreateIndex("dbo.CurrentActivities", "DurationId");
            CreateIndex("dbo.Durations", "ActivityId");
            AddForeignKey("dbo.Budgets", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CurrentActivities", "ActivityId", "dbo.Activities", "Id", cascadeDelete: false);
            AddForeignKey("dbo.CurrentActivities", "DurationId", "dbo.Durations", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Durations", "ActivityId", "dbo.Activities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Durations", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.CurrentActivities", "DurationId", "dbo.Durations");
            DropForeignKey("dbo.CurrentActivities", "ActivityId", "dbo.Activities");
            DropForeignKey("dbo.Budgets", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Durations", new[] { "ActivityId" });
            DropIndex("dbo.CurrentActivities", new[] { "DurationId" });
            DropIndex("dbo.CurrentActivities", new[] { "ActivityId" });
            DropIndex("dbo.Budgets", new[] { "ActivityId" });
            AlterColumn("dbo.Durations", "ActivityId", c => c.Int());
            AlterColumn("dbo.CurrentActivities", "DurationId", c => c.Int());
            AlterColumn("dbo.CurrentActivities", "ActivityId", c => c.Int());
            AlterColumn("dbo.Budgets", "ActivityId", c => c.Int());
            RenameColumn(table: "dbo.Durations", name: "ActivityId", newName: "Activity_Id");
            RenameColumn(table: "dbo.CurrentActivities", name: "DurationId", newName: "Duration_Id");
            RenameColumn(table: "dbo.CurrentActivities", name: "ActivityId", newName: "Activity_Id");
            RenameColumn(table: "dbo.Budgets", name: "ActivityId", newName: "Activity_Id");
            CreateIndex("dbo.Durations", "Activity_Id");
            CreateIndex("dbo.CurrentActivities", "Duration_Id");
            CreateIndex("dbo.CurrentActivities", "Activity_Id");
            CreateIndex("dbo.Budgets", "Activity_Id");
            AddForeignKey("dbo.Durations", "Activity_Id", "dbo.Activities", "Id");
            AddForeignKey("dbo.CurrentActivities", "Duration_Id", "dbo.Durations", "Id");
            AddForeignKey("dbo.CurrentActivities", "Activity_Id", "dbo.Activities", "Id");
            AddForeignKey("dbo.Budgets", "Activity_Id", "dbo.Activities", "Id");
        }
    }
}
