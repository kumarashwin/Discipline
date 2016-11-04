using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Models {
    public class ActivityAndDurationsViewModel {

        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        private Func<Activity, bool> ActivityById(int activityId) => a => a.Id == activityId;
        private Func<Activity, bool> ActivityByName(string activityName) => a => a.Name == activityName;

        public IList<Tuple<Activity, IQueryable<Duration>>> Activities { get; set; } = new List<Tuple<Activity, IQueryable<Duration>>>();

        public ActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo, string activity) {

            this.activityRepo = activityRepo;
            this.durationRepo = durationRepo;

            if (activity == null) {
                AllActivities();
            } else {
                int result;
                if (int.TryParse(activity, out result))
                    FindActivity(ActivityById(result));
                else
                    FindActivity(ActivityByName(activity));
            }
        }

        private void FindActivity(Func<Activity, bool> method) {
            var activity = activityRepo.Activities.Where(method).FirstOrDefault();
            var durations = durationRepo.Durations.Where(d => d.ActivityId == activity.Id);
            Activities.Add(new Tuple<Activity, IQueryable<Duration>>(activity, durations));
        }

        private void AllActivities() {
            var activitiesList = activityRepo.Activities.ToList();
            var durations = durationRepo.Durations;

            activitiesList.ForEach(
                activity => Activities.Add(
                    new Tuple<Activity, IQueryable<Duration>>(
                        activity, durations.Where(
                            d => d.ActivityId == activity.Id))));
        }
    }
}