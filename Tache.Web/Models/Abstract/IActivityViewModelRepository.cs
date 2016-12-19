using System;
using System.Linq;
using Tache.Web.Models.ViewModels;

namespace Tache.Web.Models.Abstract {
    public interface IActivityViewModelRepository {
        IQueryable<ActivityViewModel> Activities(DateTime dayParam);
    }
}