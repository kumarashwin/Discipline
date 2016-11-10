namespace Tache.Domain.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
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
                new Activity { Name = "reading", Description = "*...page flip*"},
                new Activity { Name = "gaming", Description = "*pew pew pew!*" });

            context.SaveChanges();

            var sleepingID = context.Activities.Where(a => a.Name == "sleeping").First().Id;
            var eatingID = context.Activities.Where(a => a.Name == "eating").First().Id;
            var codingID = context.Activities.Where(a => a.Name == "coding").First().Id;
            var readingID = context.Activities.Where(a => a.Name == "reading").First().Id;
            var gamingID = context.Activities.Where(a => a.Name == "gaming").First().Id;

            var durations = context.Durations;
            durations.RemoveRange(durations);

            var result = new List<Duration>() { };
            for (int day = 20; day < 30; day++) {
                result.Add(new Duration { ActivityId = sleepingID, From = new DateTime(2016, 10, day, 0, 0, 0), To = new DateTime(2016, 10, day, 8, 0, 0) } );
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(2016, 10, day, 8, 0, 1), To = new DateTime(2016, 10, day, 10, 0, 0) });
                result.Add(new Duration { ActivityId = eatingID, From = new DateTime(2016, 10, day, 10, 0, 1), To = new DateTime(2016, 10, day, 11, 0, 0) });
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(2016, 10, day, 11, 0, 1), To = new DateTime(2016, 10, day, 15, 0, 0) });
                result.Add(new Duration { ActivityId = gamingID, From = new DateTime(2016, 10, day, 15, 0, 1), To = new DateTime(2016, 10, day, 17, 0, 0) });
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(2016, 10, day, 17, 0, 1), To = new DateTime(2016, 10, day, 19, 0, 0) });
                result.Add(new Duration { ActivityId = eatingID, From = new DateTime(2016, 10, day, 19, 0, 1), To = new DateTime(2016, 10, day, 20, 0, 0) });
                result.Add(new Duration { ActivityId = gamingID, From = new DateTime(2016, 10, day, 20, 0, 1), To = new DateTime(2016, 10, day, 22, 0, 0) });
                result.Add(new Duration { ActivityId = readingID, From = new DateTime(2016, 10, day, 22, 0, 1), To = new DateTime(2016, 10, day, 23, 0, 0) });
                result.Add(new Duration { ActivityId = sleepingID, From = new DateTime(2016, 10, day, 23, 0, 1), To = new DateTime(2016, 10, day, 23, 59, 59) });
            }

            durations.AddRange(result);

            context.Budgets.AddOrUpdate(p => p.ActivityId,
                new Budget { ActivityId = sleepingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(8).Ticks },
                new Budget { ActivityId = eatingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(2).Ticks },
                new Budget { ActivityId = codingID, Period = Period.perWeek, TimeInTicks = TimeSpan.FromHours(35).Ticks },
                new Budget { ActivityId = gamingID, Period = Period.perWeek, TimeInTicks = TimeSpan.FromHours(25).Ticks });
        }

    }
}
