using System;
using System.Data.Entity;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Models.Abstract;
using Tache.Models.ViewModels;

namespace Tache.Models.Concrete {
    public class ActivityViewModelRepository : IActivityViewModelRepository {
        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        public ActivityViewModelRepository(IActivityRepository activityRepository, IDurationRepository durationRepository) {
            this.activityRepo = activityRepository;
            this.durationRepo = durationRepository;
        }

        public IQueryable<ActivityViewModel> Activities(DateTime dayParam) {
            var durations = durationRepo.Durations.Where(d => DbFunctions.TruncateTime(d.To) == dayParam.Date);

            return (from duration in durations
                    join activity in activityRepo.Activities
                    on duration.ActivityId equals activity.Id
                    select new ActivityViewModel {
                        Activity = activity.Id,
                        Duration = duration.Id,
                        Name = activity.Name,
                        Description = activity.Description,
                        From = duration.From,
                        To = duration.To
                    }).OrderBy(vm => vm.From);
        }
    }
}