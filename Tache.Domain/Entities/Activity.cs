using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Tache.Domain.Entities {
    public class Activity {
        private long? _budgetInTicks = null;
        private TimeSpan? _budgetHours;
        private TimeSpan? _budgetMinutes;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

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
            get {
                _budgetInTicks = _budgetInTicks ?? null;
                if (_budgetHours != null) {
                    _budgetInTicks = _budgetInTicks ?? 0;
                    _budgetInTicks += ((TimeSpan)_budgetHours).Ticks;
                }
                if (_budgetMinutes != null) {
                    _budgetInTicks = _budgetInTicks ?? 0;
                    _budgetInTicks += ((TimeSpan)_budgetMinutes).Ticks;
                }

                return _budgetInTicks;
            }
            set {
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