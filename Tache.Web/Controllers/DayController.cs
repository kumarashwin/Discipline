using System;
using System.Globalization;
using System.Web.Mvc;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDayViewModelRepository dayViewModelRepository;

        public DayController(IDayViewModelRepository dayViewModelRepository) {
            this.dayViewModelRepository = dayViewModelRepository;
        }

        private DateTime ParseDateString(string id) {
            DateTime dateTime;
            string[] format = new string[] { "MM-dd-yy", "MM-dd-yyyy", "dd-MM-yy", "dd-MM-yyyy" };

            return DateTime.TryParseExact(id, format, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out dateTime) ? dateTime : DateTime.Now;
        }

        public ActionResult Index(string id) => Json(dayViewModelRepository.Activities(ParseDateString(id)), JsonRequestBehavior.AllowGet);
    }
}