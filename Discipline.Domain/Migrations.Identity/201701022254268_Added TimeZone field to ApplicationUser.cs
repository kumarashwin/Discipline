namespace Discipline.Domain.Migrations.Identity
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTimeZonefieldtoApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TimeZone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TimeZone");
        }
    }
}
