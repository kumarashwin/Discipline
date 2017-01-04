using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;
using Discipline.Domain.Abstract;
using Discipline.Web.Models.Abstract;
using Discipline.Web.Infrastructure;

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
            DateTime dateParam = new DateTime(year, month, day);

            // This should be done when activityRepo is initialized in Home Controller, not here:
            DateTime userMidnightInUtc = TimeZoneInfo.ConvertTimeToUtc(dateParam, Extensions.GetUserTimeZone());
            activityRepo.UpdateStartUptoLastMidnight(userMidnightInUtc.AddDays(4));

            // NOTE: No checking for dates!
            // Directly tries to retrieve 10 days before and after
            // The checking needs to be done at the client-side;
            DateTime startDate = userMidnightInUtc.AddDays(-10);
            DateTime endDate = userMidnightInUtc.AddDays(10);
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