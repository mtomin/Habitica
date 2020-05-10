using Habitica;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Habitica_API.Models
{
    public class ActivityConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationID { get; set; }

        public virtual Activity Activity { get; set; }

        public Goal Goal { get; set; }

        public Difficulty Difficulty { get; set; }

        [NotMapped]
        public bool DoneToday { get; set; }
    }
}
