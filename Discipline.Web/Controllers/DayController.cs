using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Discipline.Domain.Abstract;
using Discipline.Web.Models.Abstract;

namespace Discipline.Web.Controllers {

    [Authorize(Roles = "User")]
    public class DayController : Controller {
        private IActivityRepository activityRepo;
        private IDaysViewModelRepository daysViewModelRepository;

        public DayController(IDaysViewModelRepository daysViewModelRepository, IActivityRepository activityRepo) {
            this.daysViewModelRepository = daysViewModelRepository;
            this.activityRepo = activityRepo;
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
                    // this is annoying especially as now, budgets are contained in each activity;
                    budgets = activityRepo.Activities()
                        .Where(a => a.BudgetInTicks != null)
                        .ToDictionary(a => a.Id, b => b.BudgetInTicks),

                    activities = activities
            });

            return Content(contentJson, "application/json");
        }
    }
}