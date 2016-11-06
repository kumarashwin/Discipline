using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Models {
    public class ActivityAndDurationsViewModel {
        public Activity Activity { get; set; }
        public IQueryable<Duration> Durations { get; set; }

        public ActivityAndDurationsViewModel(Activity activity, IQueryable<Duration> durations) {
            Activity = activity;
            Durations = durations;
        }
    }
}