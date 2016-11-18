using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Tache.Infrastructure.Attributes;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository) {
            this.daysViewModelRepository = daysViewModelRepository;
        }

        [ByDefaultReturnView]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public ActionResult Index(int year, int month, int day) {
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(year, month, day);
            ViewBag.ProcessedDate = startDate.ToString("yyyy-M-d");
            ViewBag.DeactivateRightArrow = "false";

            if (startDate == today)
                throw new ArgumentOutOfRangeException(paramName: null,
                    message: "We haven't yet calculated your actions for today. Come back tomorrow.");

            if (startDate > today)
                throw new ArgumentOutOfRangeException(paramName: null,
                    message: "The date you requested is too far in the future!");

            if (startDate > today.AddDays(-4)) {
                startDate = today.AddDays(-4);
                ViewBag.ProcessedDate = startDate.ToString("yyyy-M-d");
                ViewBag.DeactivateRightArrow = "true";
            }
            
            return Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(startDate)), "application/json");
        }
            
    }
}