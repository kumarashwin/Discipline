using System;
using System.Linq;
using System.Collections.Generic;
using Discipline.Web.Models.ViewModels;
using Discipline.Web.Models.Abstract;

namespace Discipline.Web.Models.Concrete {
    public class DaysViewModelRepository : IDaysViewModelRepository {
        private IActivityViewModelRepository activityViewModelRepo;

        public DaysViewModelRepository(IActivityViewModelRepository activityViewModelRepo) {
            this.activityViewModelRepo = activityViewModelRepo;
        }

        public IDictionary<string, IEnumerable<ActivityViewModel>> Days(DateTime startDate, DateTime endDate) {
            var days = new Dictionary<string, IEnumerable<ActivityViewModel>>();
            while (startDate <= endDate) {
                DateTime startDatePlusOne = startDate.AddDays(1);
                var activities = activityViewModelRepo.Activities(startDate, startDatePlusOne);
                days.Add(startDate.ToShortDateString(), activities);
                startDate = startDatePlusOne;
            }

            return days;
        }
    }
}