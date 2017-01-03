using System;
using System.Collections.Generic;
using Discipline.Web.Models.ViewModels;

namespace Discipline.Web.Models.Abstract {
    public interface IDaysViewModelRepository {
        IDictionary<string, IEnumerable<ActivityViewModel>> Days(DateTime startDate, DateTime endDate);
    }
}