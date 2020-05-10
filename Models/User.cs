using Habitica_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Habitica
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        public virtual List<ActivityConfiguration> ActivityData { get; set; }

        public virtual List<ActivityEntry> ActivityEntries { get; set; }

        [NotNull]
        [DefaultValue(0)]
        public int Experience { get; set; }

        [NotMapped]
        public string AuthToken { get; set; }

        [NotMapped]
        public string DisplayName { get; set; }
    }
}
