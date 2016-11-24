using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using Tache.Infrastructure.Filters;
using Tache.Models.Abstract;

namespace Tache.Controllers {
    public class DayController : Controller {
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository) {
            this.daysViewModelRepository = daysViewModelRepository;
        }
        /// <summary>
        /// Validates the requested date, throws exceptions if the request is for the
        /// current day or in the future.
        /// Returns ContentResult i.e. a Json string, for at most, current date less four.
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        [ByDefaultReturnView]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        public ActionResult Index(int year, int month, int day) {
            DateTime today = DateTime.Today;
            DateTime startDate, endDate;
            DateTime dateParam = new DateTime(year, month, day);
            ViewBag.DeactivateRightArrow = "false";

            //if (dateParam == today)
            //    throw new ArgumentOutOfRangeException(paramName: null,
            //        message: "We haven't yet calculated your actions for today. Come back tomorrow.");

            //if (dateParam > today)
            //    throw new ArgumentOutOfRangeException(paramName: null,
            //        message: "The date you requested is too far in the future!");

            if (dateParam > today.AddDays(-4)) {
                dateParam = today.AddDays(-4);
                ViewBag.DeactivateRightArrow = "true";
            }

            ViewBag.ProcessedDate = dateParam.ToString("yyyy-M-d");
            startDate = dateParam.AddDays(-10);
            endDate = dateParam.AddDays(10);
            endDate = endDate < today ? endDate : today.AddDays(-1);

            return Content(JsonConvert.SerializeObject(daysViewModelRepository.Days(startDate, endDate)), "application/json");
        }
            
    }
}