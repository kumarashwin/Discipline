using System;
using System.Collections.Generic;
using Discipline.Web.Models.ViewModels;

namespace Discipline.Web.Models.Abstract {
    public interface IDaysViewModelRepository {
        IDictionary<string, ICollection<ActivityViewModel>> Days(DateTime startDate, DateTime endDate);
    }
}