using System;
using System.Collections.Generic;
using Tache.Domain.Entities;

namespace Tache {
    public enum Period { perDay, perWeek };
}

namespace Tache.Domain.Abstract {
    public interface IBudgetRepository {
        IEnumerable<Budget> Budgets { get; }

        void CreateOrUpdate(Activity activity, TimeSpan timeSpan, Period period );
        Budget Delete(int budgetId);
    }
}
