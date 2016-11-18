namespace Tache.Domain.Migrations {
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tache.Domain.Concrete.DbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tache.Domain.Concrete.DbContext context) {
            context.Activities.AddOrUpdate(p => p.Name,
                new Activity { Name = "sleeping", Description = "I love sleeping!", Color = "#0099ff" },
                new Activity { Name = "eating", Description = "*chomp chomp mnggff!*", Color = "#009966" },
                new Activity { Name = "coding", Description = "*klik klak klik klik*", Color = "#3300cc" },
                new Activity { Name = "reading", Description = "*...page flip*", Color = "#333300" },
                new Activity { Name = "gaming", Description = "*pew pew pew!*", Color = "#6600cc" });

            context.SaveChanges();

            var sleepingID = context.Activities.Where(a => a.Name == "sleeping").First().Id;
            var eatingID = context.Activities.Where(a => a.Name == "eating").First().Id;
            var codingID = context.Activities.Where(a => a.Name == "coding").First().Id;
            var readingID = context.Activities.Where(a => a.Name == "reading").First().Id;
            var gamingID = context.Activities.Where(a => a.Name == "gaming").First().Id;

            int lastMinute;
            var result = new List<Duration>() { };
            for (DateTime d = (DateTime.Now).AddDays(-60); d < DateTime.Now; d = d.AddDays(1)) {
                result.Add(new Duration { ActivityId = sleepingID, From = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0), To = new DateTime(d.Year, d.Month, d.Day, 8, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(d.Year, d.Month, d.Day, 8, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 10, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = eatingID, From = new DateTime(d.Year, d.Month, d.Day, 10, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 11, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(d.Year, d.Month, d.Day, 11, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 15, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = gamingID, From = new DateTime(d.Year, d.Month, d.Day, 15, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 17, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = codingID, From = new DateTime(d.Year, d.Month, d.Day, 17, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 19, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = eatingID, From = new DateTime(d.Year, d.Month, d.Day, 19, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 20, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = gamingID, From = new DateTime(d.Year, d.Month, d.Day, 20, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 22, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = readingID, From = new DateTime(d.Year, d.Month, d.Day, 22, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = sleepingID, From = new DateTime(d.Year, d.Month, d.Day, 23, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59) });
            }

            var durations = context.Durations;
            durations.RemoveRange(durations);
            durations.AddRange(result);

            context.Budgets.AddOrUpdate(p => p.ActivityId,
            new Budget { ActivityId = sleepingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(8).Ticks },
            new Budget { ActivityId = eatingID, Period = Period.perDay, TimeInTicks = TimeSpan.FromHours(2).Ticks },
            new Budget { ActivityId = codingID, Period = Period.perWeek, TimeInTicks = TimeSpan.FromHours(35).Ticks },
            new Budget { ActivityId = gamingID, Period = Period.perWeek, TimeInTicks = TimeSpan.FromHours(25).Ticks });
        }

        private Random random = new Random();
        private int minuteRandomizer(out int lastMinute) => lastMinute = this.random.Next(0, 59);

    }
}
