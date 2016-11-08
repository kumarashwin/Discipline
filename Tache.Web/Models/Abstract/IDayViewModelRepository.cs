using System;
using System.Collections.Generic;
using Tache.Models.ViewModels;

namespace Tache.Models.Abstract {
    public interface IDayViewModelRepository {
        ICollection<ActivityViewModel> Activities(DateTime dayParam);
    }
}