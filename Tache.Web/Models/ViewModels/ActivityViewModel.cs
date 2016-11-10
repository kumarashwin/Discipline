﻿using System;

namespace Tache.Models.ViewModels {
    public class ActivityViewModel {

        public int Activity { get; set; }
        public int Duration { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}