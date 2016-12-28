using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Web.Models.Abstract;
using Tache.Web.Models.ViewModels;

namespace Tache.Web.Models.Concrete {
    public class ActivityViewModelRepository : IActivityViewModelRepository {
        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        public ActivityViewModelRepository(IActivityRepository activityRepository, IDurationRepository durationRepository) {
            this.activityRepo = activityRepository;
            this.durationRepo = durationRepository;
        }

        public IQueryable<ActivityViewModel> Activities(DateTime dayParam) =>
            (from duration in durationRepo.Durations.Where(d => DbFunctions.TruncateTime(d.To) == dayParam.Date)
             join activity in activityRepo.Activities()
             on duration.ActivityId equals activity.Id
             select new {
                 Activity = activity,
                 Duration = duration
             }).OrderBy(vm => vm.Duration.To).Select(vm => new ActivityViewModel {
                 Activity = vm.Activity.Id,
                 Duration = vm.Duration.Id,
                 Name = vm.Activity.Name,
                 Description = vm.Activity.Description,
                 From = (SqlFunctions.DateName("hh", vm.Duration.From) + ":" + SqlFunctions.DateName("mi", vm.Duration.From) + ":" + SqlFunctions.DateName("ss", vm.Duration.From)),
                 To = (SqlFunctions.DateName("hh", vm.Duration.To) + ":" + SqlFunctions.DateName("mi", vm.Duration.To) + ":" + SqlFunctions.DateName("ss", vm.Duration.To)),
                 Color = vm.Activity.Color
             });
    }
}