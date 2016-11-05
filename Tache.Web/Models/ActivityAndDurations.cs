using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Models {
    public class ActivityAndDurationsRepository {

        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        public IList<ActivityAndDurationsViewModel> Activities { get; set; } = new List<ActivityAndDurationsViewModel>();

        public ActivityAndDurationsRepository(IActivityRepository activityRepo, IDurationRepository durationRepo, string activity) {
            this.activityRepo = activityRepo;
            this.durationRepo = durationRepo;

            if (activity == null)
                AllActivities();
            else {
                int result;
                if (int.TryParse(activity, out result))
                    FindActivity(a => a.Id == result);
                else
                    FindActivity(a => a.Name == activity.ToLower());
            }
        }

        private void FindActivity(Func<Activity, bool> method) {
            var activity = activityRepo.Activities.Where(method).FirstOrDefault();
            var durations = durationRepo.Durations.Where(d => d.ActivityId == activity.Id);
            Activities.Add(new ActivityAndDurationsViewModel(activity, durations));
        }

        private void AllActivities() {
            var activitiesList = activityRepo.Activities.ToList();
            var durations = durationRepo.Durations;

            activitiesList.ForEach(
                activity => Activities.Add(
                    new ActivityAndDurationsViewModel(activity, durations.Where(
                            d => d.ActivityId == activity.Id))));
        }

    }

    public class ActivityAndDurationsViewModel {
        public Activity Activity { get; set; }
        public IQueryable<Duration> Durations { get; set; }

        public ActivityAndDurationsViewModel(Activity activity, IQueryable<Duration> durations) {
            Activity = activity;
            Durations = durations;
        }
    }
}