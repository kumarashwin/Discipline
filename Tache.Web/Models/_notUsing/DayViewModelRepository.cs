using System;
using System.Linq;
using System.Collections.Generic;
using Tache.Models.ViewModels;
using Tache.Models.Abstract;

namespace Tache.Models.Concrete {
    public class DayViewModelRepository : IDayViewModelRepository {
        private IActivityViewModelRepository activityViewModelRepo;

        public DayViewModelRepository(IActivityViewModelRepository activityViewModelRepo) {
            this.activityViewModelRepo = activityViewModelRepo;
        }

        //public IDictionary<DateTime, ICollection<ActivityViewModel>> Day(DateTime dayParam) =>
        //    new Dictionary<DateTime, ICollection<ActivityViewModel>> {
        //        { dayParam, activityViewModelRepo.Activities(dayParam).ToList() }
        //    };

        public DayViewModel Day(DateTime dayParam) =>
            new DayViewModel() {
                Day = new Dictionary<DateTime, ICollection<ActivityViewModel>>() {
                { dayParam, activityViewModelRepo.Activities(dayParam).ToList() }}
            };
    }
}