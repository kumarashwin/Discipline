using System;
using Discipline.Web.Models.ViewModels;
using System.Collections.Generic;

namespace Discipline.Web.Models.Abstract {
    public interface IActivityViewModelRepository {
        IEnumerable<ActivityViewModel> Activities(DateTime from, DateTime to);
    }
}