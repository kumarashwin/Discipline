using System;
using System.Collections.Generic;
using System.Linq;
using Discipline.Domain.Entities;
using Discipline.Web.Infrastructure;

namespace Discipline.Web.Models.ViewModels {
    public class CurrentActivityViewModel {
        private TimeSpan timeSpent;
        private bool isOverBudget = true;

        public Activity Activity { get; set; }

        public CurrentActivityViewModel(Activity activity) {
            Activity = activity;

            var serverCurrentTime = DateTime.UtcNow;
            var clientStartTime = Activity.Start.Value;

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
                var start = Activity.Start.Value.ConvertToUserTimeZone();
                return $"(Since <b>{start.ToShortTimeString()}</b> on <b>{start.ToLongDateString()}</b>.)";
            }
        }

        private string FormatHoursAndMinutes(int hours, int minutes) {
            if (hours == 0 && minutes == 0)
                return "just a few seconds";

            string hourString = null;
            string minuteString = null;

            if (hours > 0) {
                hourString = $"<b>{hours}</b>";
                if (hours == 1)
                    hourString += " hour"; 
                else
                    hourString += $" hours";
            }

            if (minutes > 0) {
                minuteString = $"<b>{minutes}</b>";
                if (minutes == 1)
                    minuteString += " minute"; 
                else
                    minuteString += $" minutes";
            }

            if (hourString != null && minuteString != null)
                return $"{hourString} and {minuteString}";
            return (hourString ?? "") + (minuteString ?? "");

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