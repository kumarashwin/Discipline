using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Models;

namespace Tache.Controllers {
    public class ActivitiesController : Controller {
        private IActivityAndDurationsRepository activityAndDurationsRepository;

        public ActivitiesController(IActivityAndDurationsRepository activityAndDurationsRepository) {
            this.activityAndDurationsRepository = activityAndDurationsRepository;
        }

        public ActionResult Index(string id) => Json(activityAndDurationsRepository.For(id).Model(), JsonRequestBehavior.AllowGet);
    }
}