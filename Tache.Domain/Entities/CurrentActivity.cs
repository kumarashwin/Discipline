using System;
using System.ComponentModel.DataAnnotations.Schema;
using Tache.Domain.Entities;

namespace Tache.Domain.Concrete {
    public class CurrentActivity {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime Start { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}