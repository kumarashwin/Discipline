using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tache.Domain.Entities {
    public class Duration {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
        [ForeignKey("ActivityId")]
        public Activity Activity { get; set; }
    }
}