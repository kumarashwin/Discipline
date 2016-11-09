using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Models.ViewModels;

namespace Tache.Models.ViewModels {
    public class DayViewModel {

        public IDictionary<DateTime, ICollection<ActivityViewModel>> Day { get; set; }
    }
}