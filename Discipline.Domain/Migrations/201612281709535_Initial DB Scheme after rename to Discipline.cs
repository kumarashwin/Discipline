namespace Discipline.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBSchemeafterrenametoDiscipline : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Color = c.String(),
                        Hide = c.Boolean(nullable: false),
                        Start = c.DateTime(),
                        BudgetInTicks = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Durations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityId = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Activities", t => t.ActivityId, cascadeDelete: true)
                .Index(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Durations", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Durations", new[] { "ActivityId" });
            DropTable("dbo.Durations");
            DropTable("dbo.Activities");
        }
    }
}
