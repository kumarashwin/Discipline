namespace Tache.Domain.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tache.Domain.Concrete.DbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tache.Domain.Concrete.DbContext context)
        {
            context.Activities.AddOrUpdate(p => p.Name,
                new Activity { Name = "sleeping", Description = "I love sleeping!" },
                new Activity { Name = "eating", Description = "*chomp chomp mnggff!*" },
                new Activity { Name = "coding", Description = "*klik klak klik klik*" },
                new Activity { Name = "reading", Description = "*...page flip*"});

            var sleepingID = context.Activities.Where(a => a.Name == "sleeping").First().Id;
            var eatingID = context.Activities.Where(a => a.Name == "eating").First().Id;
            var codingID = context.Activities.Where(a => a.Name == "coding").First().Id;
            var readingID = context.Activities.Where(a => a.Name == "reading").First().Id;

            context.Budgets.AddOrUpdate(p => p.ActivityId,
                new Budget { ActivityId = sleepingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(8).Ticks },
                new Budget { ActivityId = eatingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(2).Ticks },
                new Budget { ActivityId = codingID, Period = Period.perWeek, TimeInTicks = TimeSpan.FromHours(35).Ticks });

            context.Durations.AddOrUpdate(p => p.From,
                new Duration { ActivityId = readingID, From = new DateTime(2016, 10, 26, 21, 0, 0), To = new DateTime(2016, 10, 26, 23, 59, 59) },
                new Duration { ActivityId = readingID, From = new DateTime(2016, 10, 27, 0, 0, 0), To = new DateTime(2016, 10, 27, 1, 0, 0) },
                new Duration { ActivityId = sleepingID, From = new DateTime(2016, 10, 27, 1, 0, 1), To = new DateTime(2016, 10, 27, 9, 0, 0) },
                new Duration { ActivityId = codingID, From = new DateTime(2016, 10, 27, 9, 0, 1), To = new DateTime(2016, 10, 27, 12, 0, 0) },
                new Duration { ActivityId = eatingID, From = new DateTime(2016, 10, 27, 12, 0, 1), To = new DateTime(2016, 10, 27, 13, 0, 0) },
                new Duration { ActivityId = codingID, From = new DateTime(2016, 10, 27, 13, 0, 1), To = new DateTime(2016, 10, 27, 18, 0, 0) },
                new Duration { ActivityId = eatingID, From = new DateTime(2016, 10, 27, 18, 0, 1), To = new DateTime(2016, 10, 27, 19, 0, 0) },
                new Duration { ActivityId = readingID, From = new DateTime(2016, 10, 27, 19, 0, 1), To = new DateTime(2016, 10, 27, 23, 59, 59) },
                new Duration { ActivityId = readingID, From = new DateTime(2016, 10, 28, 0, 0, 0), To = new DateTime(2016, 10, 28, 0, 30, 0) },
                new Duration { ActivityId = sleepingID, From = new DateTime(2016, 10, 28, 0, 30, 1), To = new DateTime(2016, 10, 28, 8, 30, 0) });

        }
    }
}
