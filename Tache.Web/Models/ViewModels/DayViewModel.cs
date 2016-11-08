using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Models.ViewModels;

namespace Tache.Models.ViewModels {
    public class DayViewModel {
        public DateTime Day { get; set; }
        ICollection<ActivityViewModel> Activities { get; set; }
    }
}