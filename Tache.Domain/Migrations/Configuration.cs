namespace Tache.Domain.Migrations {
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Tache.Domain.Concrete.TacheDbContext> {
        public Configuration() {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Tache.Domain.Concrete.TacheDbContext context) {
            context.Activities.AddOrUpdate(p => p.Name,
                new Activity { Name = "Sleeping", Description = "I love sleeping!", Color = "#0099ff" },
                new Activity { Name = "Eating", Description = "*chomp chomp mnggff!*", Color = "#009966" },
                new Activity { Name = "Coding", Description = "*klik klak klik klik*", Color = "#3300cc" },
                new Activity { Name = "Reading", Description = "*...page flip*", Color = "#333300" },
                new Activity { Name = "Gaming", Description = "*pew pew pew!*", Color = "#6600cc", BudgetHours = 3 });

            context.SaveChanges();

            // Get Dict. of activity names : Id
            IDictionary<string, int> activities = GetIdOfAllActivities(context);

            // Clear all previous durations and add new randomized ones;
            var durations = context.Durations;
            durations.RemoveRange(durations);
            durations.AddRange(GetRandomDurations(activities));

            // Setting up current activity
            int codingId = activities["Coding"];
            context.Activities.Where(a => a.Id == codingId).First().Start = DateTime.Now.AddHours(-1);
        }

        private Random random = new Random();
        private int minuteRandomizer(out int lastMinute) => lastMinute = this.random.Next(0, 59);

        private Dictionary<string, int> GetIdOfAllActivities(Tache.Domain.Concrete.TacheDbContext context) =>
            new Dictionary<string, int> {
                { "Sleeping", context.Activities.Where(a => a.Name == "Sleeping").First().Id },
                { "Coding", context.Activities.Where(a => a.Name == "Coding").First().Id },
                { "Reading", context.Activities.Where(a => a.Name == "Reading").First().Id },
                { "Eating", context.Activities.Where(a => a.Name == "Eating").First().Id },
                { "Gaming", context.Activities.Where(a => a.Name == "Gaming").First().Id }
            };

        public List<Duration> GetRandomDurations(IDictionary<string, int> activities) {
            int lastMinute;
            var result = new List<Duration>() { };
            for (DateTime d = (DateTime.Today).AddDays(-60); d < DateTime.Today; d = d.AddDays(1)) {
                result.Add(new Duration { ActivityId = activities["Sleeping"], From = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0), To = new DateTime(d.Year, d.Month, d.Day, 8, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 8, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 10, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Eating"], From = new DateTime(d.Year, d.Month, d.Day, 10, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 11, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 11, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 15, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Gaming"], From = new DateTime(d.Year, d.Month, d.Day, 15, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 17, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Coding"], From = new DateTime(d.Year, d.Month, d.Day, 17, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 19, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Eating"], From = new DateTime(d.Year, d.Month, d.Day, 19, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 20, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Gaming"], From = new DateTime(d.Year, d.Month, d.Day, 20, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 22, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Reading"], From = new DateTime(d.Year, d.Month, d.Day, 22, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, minuteRandomizer(out lastMinute), 0) });
                result.Add(new Duration { ActivityId = activities["Sleeping"], From = new DateTime(d.Year, d.Month, d.Day, 23, lastMinute, 1), To = new DateTime(d.Year, d.Month, d.Day, 23, 59, 59) });
            }
            return result;
        }
    }
}
