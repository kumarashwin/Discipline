using System;
using System.Linq;
using System.Collections.Generic;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class BudgetRepository : IBudgetRepository {
        private DbContext context = new DbContext();

        public IEnumerable<Budget> Budgets {
            get {
                return context.Budgets;
            }
        }

        public void CreateOrUpdate(Activity activity, TimeSpan timeSpan, Period period) {
            var budget = this.Budgets.Where(b => b.ActivityId == activity.Id).FirstOrDefault();
            if (budget == null)
                context.Budgets.Add(new Budget {
                    ActivityId = activity.Id,
                    TimeInTicks = timeSpan.Ticks,
                    Period = period
                });
            else {
                var dbEntry = context.Budgets.Find(budget.Id);
                if (dbEntry != null) {
                    dbEntry.ActivityId = activity.Id;
                    dbEntry.TimeInTicks = timeSpan.Ticks;
                    dbEntry.Period = period;
                }
            }
            context.SaveChanges();
        }

        public Budget Delete(int budgetId) {
            var dbEntry = context.Budgets.Find(budgetId);
            if (dbEntry != null) {
                context.Budgets.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
