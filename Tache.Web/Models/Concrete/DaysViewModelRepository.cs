using System;
using System.Linq;
using System.Collections.Generic;
using Tache.Models.ViewModels;
using Tache.Models.Abstract;

namespace Tache.Models.Concrete {
    public class DaysViewModelRepository : IDaysViewModelRepository {
        private IActivityViewModelRepository activityViewModelRepo;

        public DaysViewModelRepository(IActivityViewModelRepository activityViewModelRepo) {
            this.activityViewModelRepo = activityViewModelRepo;
        }

        public IDictionary<string, ICollection<ActivityViewModel>> Days(DateTime startDate) {

            if (startDate > DateTime.Now.AddDays(-7))
                startDate = DateTime.Now.AddDays(-7);
            
            var days = new Dictionary<string, ICollection<ActivityViewModel>>();

            for (int i = -3; i <= 3; i++) {
                var day = startDate.AddDays(i);
                days.Add(day.ToShortDateString(), activityViewModelRepo.Activities(day).ToList());
            }
                
            return days;
        }
    }
}