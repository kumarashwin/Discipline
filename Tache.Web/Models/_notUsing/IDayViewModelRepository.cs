using System;
using System.Collections.Generic;
using Tache.Models.ViewModels;

namespace Tache.Models.Abstract {
    public interface IDayViewModelRepository {
        DayViewModel Day(DateTime dayParam);
    }
}