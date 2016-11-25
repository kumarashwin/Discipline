using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Models.ViewModels;

namespace Tache.Controllers {
    public class HomeController : Controller {
        private IActivityRepository activityRepo;
        private ICurrentActivityRepository currentActivityRepo;

        public HomeController(ICurrentActivityRepository currentActivityRepo, IActivityRepository activityRepo) {
            this.currentActivityRepo = currentActivityRepo;
            this.activityRepo = activityRepo;
        }

        public ActionResult Index() => View(model: 
            (from activity in activityRepo.Activities
             join cA in currentActivityRepo.CurrentActivities
             on activity.Id equals cA.ActivityId
             into joined
             from currentActivity in joined.DefaultIfEmpty()
             select new CurrentActivityViewModel {
                 Activity = activity,
                 CurrentActivity = currentActivity,
             }).ToList());
    }
}