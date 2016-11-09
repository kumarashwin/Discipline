using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Models.ViewModels;

namespace Tache.Models.ViewModels {
    public class DaysViewModel {
        ICollection<DayViewModel> Days { get; set; }
    }
}