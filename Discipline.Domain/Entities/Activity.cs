using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Discipline.Domain.Entities {
    public class Activity {
        private long? _budgetInTicks = null;
        private TimeSpan? _budgetHours;
        private TimeSpan? _budgetMinutes;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public string UserName { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        [ScaffoldColumn(false)]
        public bool Hide { get; set; } = false;

        [HiddenInput(DisplayValue = false)]
        public DateTime? Start { get; set; } = null;

        [ScaffoldColumn(false)]
        public long? BudgetInTicks {
            // GET will be called by EF when deciding to store the value
            // in the database
            get {
                //_budgetInTicks = _budgetInTicks ?? null;
                long newBudget = 0;
                bool changed = false;

                if (_budgetHours != null) {
                    newBudget += ((TimeSpan)_budgetHours).Ticks;
                    changed = true;
                }

                if (_budgetMinutes != null) {
                    newBudget += ((TimeSpan)_budgetMinutes).Ticks;
                    changed = true;
                }

                // If values were put in budgetHours and budgetMinutes
                // budgetInTicks should accordingly change
                if (changed)
                    _budgetInTicks = newBudget;

                return _budgetInTicks;
            }
            set {
                // Only Entity Framework should be setting this value
                // usually the first time an Activity object is instanced
                _budgetInTicks = value;
            }
        }

        [NotMapped]
        [Display(Name = "Hours")]
        public int? BudgetHours {
            get {
                if (_budgetInTicks != null)
                    return new TimeSpan((long)_budgetInTicks).Hours;
                return null;
            }
            set {
                _budgetHours = null;
                if (value != null)
                    _budgetHours = TimeSpan.FromHours((int)value);
            }
        }

        [NotMapped]
        [Display(Name = "Minutes")]
        public int? BudgetMinutes {
            get {
                if (_budgetInTicks != null)
                    return new TimeSpan((long)_budgetInTicks).Minutes;
                return null;
            }
            set {
                _budgetMinutes = null;
                if (value != null)
                    _budgetMinutes = TimeSpan.FromMinutes((int)value);
            }
        }
    }
}