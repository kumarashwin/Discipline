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

        public IDictionary<string, ICollection<ActivityViewModel>> Days(DateTime startDate, DateTime endDate) {
            var days = new Dictionary<string, ICollection<ActivityViewModel>>();

            for (; startDate <= endDate; startDate = startDate.AddDays(1)) 
                days.Add(
                    startDate.ToShortDateString(),
                    activityViewModelRepo.Activities(startDate).ToList());
            
            return days;
        }
    }
}