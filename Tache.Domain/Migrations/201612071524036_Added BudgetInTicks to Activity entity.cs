namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBudgetInTickstoActivityentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "BudgetInTicks", c => c.Long());
            DropColumn("dbo.Activities", "Default");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Activities", "Default", c => c.Boolean(nullable: false));
            DropColumn("dbo.Activities", "BudgetInTicks");
        }
    }
}
