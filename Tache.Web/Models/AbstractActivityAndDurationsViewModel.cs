using System.Collections.Generic;
using Tache.Domain.Abstract;

namespace Tache.Models {
    public abstract class AbstractActivityAndDurationsViewModel {
        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        public IList<Tuple<Activity, IQueryable<Duration>>> Activities { get; set; } = new List<Tuple<Activity, IQueryable<Duration>>>();

        public AbstractActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo) {
            this.activityRepo = activityRepo;
            this.durationRepo = durationRepo;
        }
    }
}