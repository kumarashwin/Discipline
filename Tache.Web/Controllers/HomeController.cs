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
            // Inefficient, optimize this:
            activityRepo.Stop(int.Parse(currentActivity), time);
            activityRepo.Start(int.Parse(newActivity), time.AddSeconds(1));
            ModelState.Clear();
            return PartialView("Body", activityRepo.Activities);
        }

        public ActionResult Index() => View(model: activityRepo.Activities);

        //[HttpPost]
        //public JsonResult Delete(int activityId) => Json(true);

        //[HttpPost]
        //public bool Delete(int activityId) => (activityRepo.Delete(activityId)) != null;
         
    }
}