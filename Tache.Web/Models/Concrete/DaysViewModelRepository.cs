using System;
using System.Linq;
using System.Collections.Generic;
using Tache.Models.ViewModels;
using Tache.Models.Abstract;
using System.Globalization;

namespace Tache.Models.Concrete {
    public class DaysViewModelRepository : IDaysViewModelRepository {
        private IActivityViewModelRepository activityViewModelRepo;

        public DaysViewModelRepository(IActivityViewModelRepository activityViewModelRepo) {
            this.activityViewModelRepo = activityViewModelRepo;
        }

        private DateTime ParseDateString(string dayParam) {
            DateTime dateTime;
            string[] format = new string[] { "MM-dd-yy", "MM-dd-yyyy", "dd-MM-yy", "dd-MM-yyyy" };

            return DateTime.TryParseExact(dayParam, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateTime) ? dateTime : DateTime.Now;
        }

        public IDictionary<string, ICollection<ActivityViewModel>> Days(string dayParam) {
            DateTime startDate = ParseDateString(dayParam);
            var days = new Dictionary<string, ICollection<ActivityViewModel>>();

            for (int i = -3; i <= 3; i++) {
                var day = startDate.AddDays(i);
                days.Add(day.ToShortDateString(), activityViewModelRepo.Activities(day).ToList());
            }
                
            return days;
        }
    }
}