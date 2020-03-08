using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica
{
    public class Activity
    {
        public string ActivityID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int WeeklyFrequency { get; set; }

        public Difficulty Difficulty { get; set; }

        public bool DoneToday { get; set; }
    }
}
