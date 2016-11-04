using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tache.Domain.Entities {
    public class Budget {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public long TimeInTicks { get; set; }
        public Period Period { get; set; }

        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}