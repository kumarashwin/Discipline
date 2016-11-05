using Newtonsoft.Json;
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

        public ActionResult Index(string id) => Json(new ActivityAndDurationsRepository(activityRepo, durationRepo, id).Activities, JsonRequestBehavior.AllowGet);
    }
}