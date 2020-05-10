using Habitica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica_API.Models
{
    public class ActivityEntry
    {
        [Key]
        public int RecordID { get; set; }
       // public virtual User User { get; set; }

        public virtual Activity Activity { get; set; }

        [NotNull]
        public DateTime Timestamp { get; set; }
    }
}
