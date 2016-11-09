using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
                        From = (SqlFunctions.DateName("hh", duration.From) + ":" + SqlFunctions.DateName("mi", duration.From) + ":" + SqlFunctions.DateName("ss", duration.From)),
                        To = (SqlFunctions.DateName("hh", duration.To) + ":" + SqlFunctions.DateName("mi", duration.To) + ":" + SqlFunctions.DateName("ss", duration.To))
                    }).OrderBy(vm => vm.From);
        }
    }
}