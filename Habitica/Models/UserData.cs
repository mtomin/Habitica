using System;
using System.Collections.Generic;

namespace Habitica
{
    public class UserData
    {
        public string UserId { get; set; }

        public List<Activity> Activities { get; set; }

        public int Experience { get; set; }
    }
}
