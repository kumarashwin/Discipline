using System;

namespace Tache.Models.ViewModels {
    public class ActivityViewModel {

        public int Activity { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}