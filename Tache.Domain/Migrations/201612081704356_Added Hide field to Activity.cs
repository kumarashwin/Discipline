namespace Tache.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHidefieldtoActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Hide", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Hide");
        }
    }
}
