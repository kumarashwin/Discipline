using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Models {
    public class ActivityAndDurationsRepository : IActivityAndDurationsRepository {

        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;
        private string activityParam;

        public ICollection<ActivityAndDurationsViewModel> Model() {
            if (activityParam == null)
                return AllActivities();
            else {
                int result;
                if (int.TryParse(activityParam, out result))
                    return FindActivity(a => a.Id == result);
                else
                    return FindActivity(a => a.Name == activityParam.ToLower());
            }
        }

        public ActivityAndDurationsRepository(IActivityRepository activityRepo, IDurationRepository durationRepo) {
            this.activityRepo = activityRepo;
            this.durationRepo = durationRepo;
        }

        public ActivityAndDurationsRepository For(string activityParam) {
            this.activityParam = activityParam;
            return this;
        }

        private ICollection<ActivityAndDurationsViewModel> FindActivity(Func<Activity, bool> method) {
            var activity = activityRepo.Activities.Where(method).FirstOrDefault();
            var durations = durationRepo.Durations.Where(d => d.ActivityId == activity.Id);

            return new List<ActivityAndDurationsViewModel>() { new ActivityAndDurationsViewModel(activity, durations) };
        }

        private ICollection<ActivityAndDurationsViewModel> AllActivities() {
            var activitiesList = activityRepo.Activities.ToList();
            var durations = durationRepo.Durations;
            var activities = new List<ActivityAndDurationsViewModel>();

            activitiesList.ForEach(
                activity => activities.Add(
                    new ActivityAndDurationsViewModel(activity, durations.Where(
                            d => d.ActivityId == activity.Id))));

            return activities;
        }
    }
}