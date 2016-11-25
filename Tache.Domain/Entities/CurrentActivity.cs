using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tache.Domain.Entities {
    public class CurrentActivity {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime Start { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}