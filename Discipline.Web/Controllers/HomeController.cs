using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Discipline.Domain.Abstract;
using Discipline.Domain.Entities;
using Discipline.Web.Models.ViewModels;

namespace Discipline.Web.Controllers {

    [Authorize(Roles = "User")]
    public class HomeController : Controller {
        private IActivityRepository activityRepo;

        public HomeController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public ActionResult Index() => View(model: activityRepo.Activities());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string newActivity, string clientRequestTime) {
            var time = DateTime.Parse(clientRequestTime);
            activityRepo.StartNew(int.Parse(newActivity), time.AddSeconds(1));
            ModelState.Clear();
            return PartialView("ActivityStatus", activityRepo.Activities());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Activity activity) {
            if (activity.UserName == null)
                activity.UserName = User.Identity.Name;
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