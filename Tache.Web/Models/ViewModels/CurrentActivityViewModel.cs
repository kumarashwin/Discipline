using System;
using System.Collections.Generic;
using System.Linq;
using Tache.Domain.Entities;

namespace Tache.Web.Models.ViewModels {
    public class CurrentActivityViewModel {
        private TimeSpan timeSpent;
        private bool isOverBudget = true;

        public Activity Activity { get; set; }

        public CurrentActivityViewModel(IEnumerable<Activity> activities) {
            Activity = activities.Where(e => e.Start != null).First();

            var serverCurrentTime = DateTime.UtcNow;
            var clientStartTime = TimeZoneInfo.ConvertTimeToUtc(Activity.Start.Value);

            if (serverCurrentTime > clientStartTime)
                timeSpent = serverCurrentTime - clientStartTime;
            else
                timeSpent = clientStartTime - serverCurrentTime;
        }

        public string CalculateDifferenceFromBudget() {
            long ticks = timeSpent.Ticks - (long)Activity.BudgetInTicks;

            if (ticks <= 0) {
                isOverBudget = false;
                ticks *= -1;
            }

            TimeSpan timeSpan = new TimeSpan(ticks);
            return FormatHoursAndMinutes(timeSpan.Hours, timeSpan.Minutes);
        }

        public string StartDateTime {
            get {
                var start = (DateTime)Activity.Start;
                return $"(Since <b>{start.ToShortTimeString()}</b> on <b>{start.ToLongDateString()}</b>.)";
            }
        }

        private string FormatHoursAndMinutes(int hours, int minutes) {
            var result = "";
            if (hours == 0 && minutes == 0)
                return "just a few seconds";
            if (hours > 0) {
                switch (hours) {
                    case 1:
                        result += $"<b>{hours}</b> hour";
                        break;
                    default:
                        result += $"<b>{hours}</b> hours";
                        break;
                }
            }

            if (minutes > 0) {
                result += " and ";
                switch (minutes) {
                    case 1:
                        result += $"<b>{minutes}</b> minute";
                        break;
                    default:
                        result += $"<b>{minutes}</b> minutes";
                        break;
                }
            }
            return result;
        }

        public string TimeSpent {
            get {
                string result = "";
                result += $"You have been <b>{ Activity.Name }</b> for ";
                result += FormatHoursAndMinutes(timeSpent.Hours, timeSpent.Minutes);
                result += ".";
                return result;
            }
        }

        private string Budget {
            get {
                return FormatHoursAndMinutes((int)Activity.BudgetHours, (int)Activity.BudgetMinutes);
            }
        }

        public string BudgetStatus {
            get {
                if (Activity.BudgetInTicks == null)
                    return "You haven't set a budget for this activity.";

                string differenceFromBudget = CalculateDifferenceFromBudget();
                if (isOverBudget)
                    return $"You are over your budget of {Budget} by {differenceFromBudget}.";
                else
                    return $"You have {differenceFromBudget} left before you reach your budget of {Budget}.";
            }
        }

    }
}