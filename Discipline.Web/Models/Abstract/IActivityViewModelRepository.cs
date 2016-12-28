using System;
using System.Linq;
using Discipline.Web.Models.ViewModels;

namespace Discipline.Web.Models.Abstract {
    public interface IActivityViewModelRepository {
        IQueryable<ActivityViewModel> Activities(DateTime dayParam);
    }
}