using System;
using System.Linq;
using System.Web.Mvc;
using Discipline.Domain.Abstract;
using Discipline.Domain.Entities;
using System.Globalization;
using Discipline.Web.Infrastructure;

namespace Discipline.Web.Controllers {

    [Authorize(Roles = "User, Admin")]
    public class HomeController : Controller {
        private IActivityRepository activityRepo;

        public HomeController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public ActionResult Index() {
            // If this is the users first time accessing the system,
            // make a default activity and set it to current
            if (activityRepo.Activities().Count() == 0)
                return RedirectToAction("Initial");
            return View(model: activityRepo.Activities());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string newActivity, string clientRequestTime) {
            activityRepo.StartNew(
                int.Parse(newActivity),
                long.Parse(clientRequestTime).FromUnixTimeToUtc().AddSeconds(1));

            ModelState.Clear();
            return PartialView("ActivityStatus", activityRepo.Activities());
        }

        public ActionResult Initial() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Initial(string currentTime) {
            long test = long.Parse(currentTime);
            DateTime start = test.FromUnixTimeToUtc();
            activityRepo.CreateOrUpdate(new Activity { Name = "on the internet", Description = "Being productive?", Color = "#0066cc", Start = start});
            ModelState.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Activity activity) {
            activityRepo.CreateOrUpdate(activity);
            ModelState.Clear();
            return PartialView("NextActivity", activityRepo.Activities(true));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            activityRepo.Hide(id);
            ModelState.Clear();
            return PartialView("NextActivity", activityRepo.Activities(true));
        }
    }
}