using System;
using System.Collections.Generic;
using Tache.Web.Models.ViewModels;

namespace Tache.Web.Models.Abstract {
    public interface IDaysViewModelRepository {
        IDictionary<string, ICollection<ActivityViewModel>> Days(DateTime startDate, DateTime endDate);
    }
}