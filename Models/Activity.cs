using System.ComponentModel.DataAnnotations.Schema;

namespace Habitica
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDefault { get; set; }
    }
}
