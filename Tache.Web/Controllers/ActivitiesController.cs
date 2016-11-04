using System;
using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Models;

namespace Tache.Controllers {
    public class ActivitiesController : Controller {
        private IActivityRepository activityRepo;
        private IDurationRepository durationRepo;

        public ActivitiesController(IActivityRepository activityRepo, IDurationRepository durationRepo) {
            this.activityRepo = activityRepo;
            this.durationRepo = durationRepo;
        }

        public ActionResult Index(string id) {
            if (id == null)
                return View(new ActivityAndDurationsViewModel(activityRepo, durationRepo));
            else {
                int result;
                return int.TryParse(id, out result) ?
                    View(new ActivityAndDurationsViewModel(activityRepo, durationRepo, result)) :
                    View(new ActivityAndDurationsViewModel(activityRepo, durationRepo, id.ToLower()));
            }
        }
    }
}