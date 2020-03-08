using System;
using System.Collections.Generic;

namespace Habitica
{
    public class User
    {
        public string UserId { get; set; }

        public virtual List<Activity> Activities { get; set; }

        public int Experience { get; set; }
    }
}
