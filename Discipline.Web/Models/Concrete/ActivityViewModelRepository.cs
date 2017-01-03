using System;
using System.Web;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Discipline.Domain.Abstract;
using Discipline.Web.Models.Abstract;
using Discipline.Web.Models.ViewModels;
using Discipline.Domain.Entities;
using Discipline.Web.Infrastructure;
using System.Globalization;

namespace Discipline.Web.Models.Concrete {
    public class ActivityViewModelRepository : IActivityViewModelRepository {
        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;
        private ApplicationUser user;

        public ActivityViewModelRepository(IActivityRepository activityRepository, IDurationRepository durationRepository) {
            this.activityRepo = activityRepository;
            this.durationRepo = durationRepository;
            this.user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
        }

        public IEnumerable<ActivityViewModel> Activities(DateTime dayParam) =>
            (from duration in durationRepo.Durations.Where(d => DbFunctions.TruncateTime(d.To) == dayParam.Date)
             join activity in activityRepo.Activities()
             on duration.ActivityId equals activity.Id
             select new { Activity = activity, Duration = duration })
            .ToList()
            .OrderBy(vm => vm.Duration.From)
            .Select(vm => new ActivityViewModel {
                Activity = vm.Activity.Id,
                Duration = vm.Duration.Id,
                Name = vm.Activity.Name,
                Description = vm.Activity.Description,
                From = vm.Duration.From.ConvertToUserTimeZone().ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                To = vm.Duration.To.ConvertToUserTimeZone().ToString("HH:mm:ss"),
                Color = vm.Activity.Color
            });
    }
}