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
        public ActionResult Index(int year, int month, int day) {
            DateTime startDate, endDate, dateParam;

            // NOTE: Does no checking for dates; directly tries to retrieve 10 days before and after
            // Thus, the checking needs to be done at the JavaScript side;
            dateParam = new DateTime(year, month, day);
            startDate = dateParam.AddDays(-10);
            endDate = dateParam.AddDays(10);

            var activities = daysViewModelRepository.Days(startDate, endDate);

            string contentJson = JsonConvert.SerializeObject( new {
                    activities = activities,
                    budgets = context.Activities.Where(a => a.BudgetInTicks != null).ToDictionary(a => a.Id, b => b.BudgetInTicks)});

            return Content(contentJson, "application/json");
        }

    }
}