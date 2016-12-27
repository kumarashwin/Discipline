using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;
using Tache.Web.Models.ViewModels;

namespace Tache.Web.Controllers {

    [Authorize(Roles = "User")]
    public class HomeController : Controller {
        private IActivityRepository activityRepo;

        public HomeController(IActivityRepository activityRepo) {
            this.activityRepo = activityRepo;
        }

        public ActionResult Index() => View(model: activityRepo.Activities);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string newActivity, string clientRequestTime) {
            var time = DateTime.Parse(clientRequestTime);
            activityRepo.StartNew(int.Parse(newActivity), time.AddSeconds(1));
            ModelState.Clear();
            return PartialView("ActivityStatus", activityRepo.Activities);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Activity activity) {
            activityRepo.CreateOrUpdate(activity);
            ModelState.Clear();
            return PartialView("NextActivity", activityRepo.Activities.Where(a => a.Hide == false && a.Start == null));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id) {
            activityRepo.Hide(id);
            ModelState.Clear();
            return PartialView("NextActivity", activityRepo.Activities.Where(a => a.Hide == false && a.Start == null));
        }
    }
}