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
                new Activity { UserName = "kumar.ashwin@outlook.com", Name = "Sleeping", Description = "I love sleeping!", Color = "#0099ff" },
                new Activity { UserName = "kumar.ashwin@outlook.com", Name = "Eating", Description = "*chomp chomp mnggff!*", Color = "#009966" },
                new Activity { UserName = "kumar.ashwin@outlook.com", Name = "Coding", Description = "*klik klak klik klik*", Color = "#3300cc" },
                new Activity { UserName = "kumar.ashwin@outlook.com", Name = "Reading", Description = "*...page flip*", Color = "#333300" },
                new Activity { UserName = "kumar.ashwin@outlook.com", Name = "Gaming", Description = "*pew pew pew!*", Color = "#6600cc", BudgetHours = 3 },
                new Activity { UserName = "katherine.pineault@gmail.com", Name = "Working", Color= "#9F6164" },
                new Activity { UserName = "katherine.pineault@gmail.com", Name = "Cuddling", Color= "#6F3662" },
                new Activity { UserName = "katherine.pineault@gmail.com", Name = "Teaching", Color= "#FF7182" },
                new Activity { UserName = "katherine.pineault@gmail.com", Name = "Thinking", Color= "#FFAE5D" },
                new Activity { UserName = "katherine.pineault@gmail.com", Name = "Napping", Color= "#F8F8F2" }
            );

            context.SaveChanges();

            Console.WriteLine("REACHING HERE");
            // Get Dict. of activity names : Id
            IDictionary<string, int> activities = GetIdOfAllActivities(context);

            // Clear all previous durations and add new randomized ones;
            var durations = context.Durations;
            durations.RemoveRange(durations);
            durations.AddRange(GetRandomDurations(activities));

            // Adding a day's worth of activities for Katherine
            var yesterday = DateTime.Today.AddDays(-1);
            durations.AddRange(
                new List<Duration>() {
                    new Duration { ActivityId = activities["Napping"], From = yesterday, To = yesterday.AddHours(7) },
                    new Duration { ActivityId = activities["Cuddling"], From = yesterday.AddHours(7).AddSeconds(1), To = yesterday.AddHours(8) },
                    new Duration { ActivityId = activities["Working"], From = yesterday.AddHours(8).AddSeconds(1), To = yesterday.AddHours(9) },
                    new Duration { ActivityId = activities["Thinking"], From = yesterday.AddHours(14).AddSeconds(1), To = yesterday.AddHours(15) },
                    new Duration { ActivityId = activities["Teaching"], From = yesterday.AddHours(15).AddSeconds(1), To = yesterday.AddHours(20) },
                    new Duration { ActivityId = activities["Thinking"], From = yesterday.AddHours(20).AddSeconds(1), To = yesterday.AddHours(21) },
                    new Duration { ActivityId = activities["Cuddling"], From = yesterday.AddHours(21).AddSeconds(1), To = yesterday.AddHours(22) },
                    new Duration { ActivityId = activities["Napping"], From = yesterday.AddHours(22).AddSeconds(1), To = yesterday.AddHours(24 + 9) },
                }
            );

            // Setting up current activity
            context.Activities.Where(a => a.Name == "Coding").First().Start = DateTime.Now.AddHours(-1);
            context.Activities.Where(a => a.Name == "Working").First().Start = DateTime.Today.AddHours(9).AddSeconds(1);
        }

        private Random random = new Random();
        private int minuteRandomizer(out int lastMinute) => lastMinute = this.random.Next(0, 59);

        private Dictionary<string, int> GetIdOfAllActivities(Tache.Domain.Concrete.TacheDbContext context) =>
            new Dictionary<string, int> {
                { "Sleeping", context.Activities.Where(a => a.Name == "Sleeping").First().Id },
                { "Coding", context.Activities.Where(a => a.Name == "Coding").First().Id },
                { "Reading", context.Activities.Where(a => a.Name == "Reading").First().Id },
                { "Eating", context.Activities.Where(a => a.Name == "Eating").First().Id },
                { "Gaming", context.Activities.Where(a => a.Name == "Gaming").First().Id },
                { "Working", context.Activities.Where(a => a.Name == "Working").First().Id },
                { "Napping", context.Activities.Where(a => a.Name == "Napping").First().Id },
                { "Thinking", context.Activities.Where(a => a.Name == "Thinking").First().Id },
                { "Cuddling", context.Activities.Where(a => a.Name == "Cuddling").First().Id },
                { "Teaching", context.Activities.Where(a => a.Name == "Teaching").First().Id }
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
