using System;
using System.Collections.Generic;
using Tache.Models.ViewModels;

namespace Tache.Models.Abstract {
    public interface IDaysViewModelRepository {
        IDictionary<string, ICollection<ActivityViewModel>> Days(DateTime startDate, DateTime endDate);
    }
}