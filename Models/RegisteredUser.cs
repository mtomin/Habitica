using Habitica;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Habitica_API.Models
{
    public class RegisteredUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [MaxLength(20)]
        public string Username { get; set; }

        [JsonIgnore]
        [MaxLength(100)]
        public string PasswordHash { get; set; }

        public virtual User User { get; set; }

    }
}
