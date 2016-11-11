using System.ComponentModel.DataAnnotations.Schema;

namespace Tache.Domain.Entities {
    public class Activity {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
    }
}