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

        public ICollection<ActivityViewModel> Activities(DateTime dayParam) =>
            activityViewModelRepo.Activities(dayParam).ToList();
    }
}