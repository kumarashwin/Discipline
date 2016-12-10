using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Tache.Domain.Abstract;
using Tache.Infrastructure.Filters;
using Tache.Models.Abstract;
using Tache.Models.ViewModels;

namespace Tache.Controllers {
    public class DayController : Controller {
        private AbstractDbContext context;
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository, AbstractDbContext context) {
            this.daysViewModelRepository = daysViewModelRepository;
            this.context = context;
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
        //[ByDefaultReturnView]
        //[HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        [HttpPost]
        public ActionResult Index(int year, int month, int day) {

            DateTime startDate, endDate;
            DateTime dateParam = new DateTime(year, month, day);

            var deactivateRightArrow = "false";
            if (dateParam > DateTime.Today.AddDays(-4)) {
                dateParam = DateTime.Today.AddDays(-4);
                deactivateRightArrow  = "true";
            }
            var processedDate = dateParam.ToString("yyyy-M-d");

            startDate = dateParam.AddDays(-10);
            endDate = dateParam.AddDays(10);
            endDate = endDate < DateTime.Today ? endDate : DateTime.Today.AddDays(-1);
            var activities = daysViewModelRepository.Days(startDate, endDate);

            var budgets = context.Budgets.Where(budget => budget.Period == Period.perDay).ToDictionary(b => b.ActivityId, model => model.TimeInTicks);

            var contentJson = new {
                activities = activities,
                budgets = budgets,
                processedDate = processedDate,
                deactivateRightArrow = deactivateRightArrow
            };

            return Content(JsonConvert.SerializeObject(contentJson), "application/json");
        }
            
    }
}