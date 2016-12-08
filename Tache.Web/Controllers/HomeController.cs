using System;
using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Controllers {
    public class HomeController : Controller {
        private IActivityRepository activityRepo;

        public HomeController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public ActionResult Index() => View(model: activityRepo.Activities);

        [HttpPost]
        public ActionResult Index(string currentActivity, string newActivity) {
            var time = DateTime.UtcNow;
            // Inefficient, optimize this:
            activityRepo.Stop(int.Parse(currentActivity), time);
            activityRepo.Start(int.Parse(newActivity), time.AddSeconds(1));
            ModelState.Clear();
            return PartialView("Body", activityRepo.Activities);
        }

        [HttpPost]
        public ActionResult Update(Activity activity) {
            activityRepo.CreateOrUpdate(activity);
            ModelState.Clear();
            return PartialView("Body", activityRepo.Activities);
        }

        [HttpPost]
        public ActionResult Delete(int id) {
            activityRepo.Hide(id);
            ModelState.Clear();
            return PartialView("Body", activityRepo.Activities);
        }
    }
}