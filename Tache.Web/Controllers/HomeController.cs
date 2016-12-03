using System;
using System.Web.Mvc;
using Tache.Domain.Abstract;

namespace Tache.Controllers {
    public class HomeController : Controller {
        private IActivityRepository activityRepo;

        public HomeController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        [HttpPost]
        public ActionResult Index(string currentActivity, string newActivity) {
            var time = DateTime.UtcNow;
            activityRepo.Stop(int.Parse(currentActivity), time);
            activityRepo.Start(int.Parse(newActivity), time.AddSeconds(1));
            return RedirectToAction("Index");
        }

        public ActionResult Index() => View(model: activityRepo.Activities);
    }
}