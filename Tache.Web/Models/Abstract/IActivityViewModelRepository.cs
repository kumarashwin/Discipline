using System;
using System.Linq;
using Tache.Models.ViewModels;

namespace Tache.Models.Abstract {
    public interface IActivityViewModelRepository {
        IQueryable<ActivityViewModel> Activities(DateTime dayParam);
    }
}