using Habitica;
using Habitica_API.DataAccess;
using Habitica_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica_API.Helpers
{
    public static class ActivitiesHelper
    {
        public static List<ActivityConfiguration> DefaultActivities(DatabaseContext _context)
        {
            var activityConfigurations = new List<ActivityConfiguration>();
            foreach(var activity in _context.Activities.Where(a => a.IsDefault))
            {
                activityConfigurations.Add(new ActivityConfiguration()
                {
                    Activity=activity,
                    Difficulty=Difficulty.Medium,
                    DoneToday=false,
                    Goal=Goal.Weekly
                });
            }

            return activityConfigurations;
        }
    }
}
