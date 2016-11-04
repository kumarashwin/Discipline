using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Models {
    public class ActivityAndDurationsViewModel {
        
        public IList<Tuple<Activity, IQueryable<Duration>>> Activities { get; set; } = new List<Tuple<Activity, IQueryable<Duration>>>();

        public ActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo, int activityId) : this(activityRepo, durationRepo, a => a.Id == activityId) { }
        public ActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo, string activityName) : this(activityRepo, durationRepo, a => a.Name == activityName) { }
        private ActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo, Func<Activity, bool> method) {
            var activity = activityRepo.Activities.Where(method).FirstOrDefault();
            var durations = durationRepo.Durations.Where(d => d.ActivityId == activity.Id);
            Activities.Add(new Tuple<Activity, IQueryable<Duration>>(activity, durations));
        }

        public ActivityAndDurationsViewModel(IActivityRepository activityRepo, IDurationRepository durationRepo) {

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