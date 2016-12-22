using System;
using System.Linq;
using Tache.Domain.Abstract;
using Tache.Domain.Entities;

namespace Tache.Web.Models.ViewModels {
    public class CurrentActivityViewModel {
        private TimeSpan timeSpent;
        private string differenceFromBudget;
        private bool isOverBudget = true;

        public CurrentActivityViewModel(IActivityRepository activities) {
            Activity = activities.Activities.Where(e => e.Start != null).First();

            var serverCurrentTime = DateTime.UtcNow;
            var clientStartTime = TimeZoneInfo.ConvertTimeToUtc(Activity.Start.Value);

            if (serverCurrentTime > clientStartTime)
                timeSpent = serverCurrentTime - clientStartTime;
            else
                timeSpent = clientStartTime - serverCurrentTime;
        }

        public string DifferenceFromBudget {
            get {
                if (Activity.BudgetInTicks != null) {
                    long ticks = timeSpent.Ticks - (long)Activity.BudgetInTicks;

                    if (ticks <= 0) {
                        isOverBudget = false;
                        ticks *= -1;
                    }

                    var timeSpan = new TimeSpan(ticks);
                    return FormatHoursAndMinutes(timeSpan.Hours, timeSpan.Minutes);
                } else {
                    throw new Exception("Code shouldn't call difference from budget if no budget");
                }
            }
        }

        public string StartDateTime {
            get {
                var start = (DateTime)Activity.Start;
                return $"(Since <b>{start.ToShortTimeString()}</b> on <b>{start.ToLongDateString()}</b>.)";
            }
        }

        private string FormatHoursAndMinutes(int hours, int minutes) {
            var result = "";
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
                if (isOverBudget)
                    return $"You are over your budget of {Budget} by {DifferenceFromBudget}.";
                else
                    return $"You have {DifferenceFromBudget} left before you reach your budget of {Budget}.";
            }
        }

        public Activity Activity { get; set; }
    }
}