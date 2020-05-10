using Habitica;
using Habitica_API.DataAccess;
using Habitica_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Habitica_API.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private DatabaseContext _context;
        public ActivityRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void CreateActivity(ActivityConfiguration activityConfiguration)
        {
            _context.ActivityConfiguration.Add(activityConfiguration);
        }

        public void DeleteActivity(int activityId)
        {
            var activityToDelete = _context.ActivityConfiguration.First(a => a.Activity.ActivityID == activityId);
            _context.ActivityConfiguration.Remove(activityToDelete);
            _context.SaveChanges();
        }

        public List<ActivityConfiguration> GetUserActivities(int userId)
        {
            var user = _context.Users.Find(userId);
            var activitiesDoneToday = _context.ActivityEntries.Where(ua => ua.Timestamp >= DateTime.Today).Select(a => a.Activity);
            user.ActivityData.ForEach(ad => ad.DoneToday = activitiesDoneToday.Contains(ad.Activity));
            return user.ActivityData;
        }

        public ActivityConfiguration UpdateActivity(ActivityConfiguration newActivityConfiguration, int userID)
        {
            var activityConfigurationToChange = _context.ActivityConfiguration.First(ac => ac == newActivityConfiguration);
            activityConfigurationToChange.DoneToday = _context.ActivityEntries.Any(ae => ae.Activity.Equals(activityConfigurationToChange.Activity) && ae.Timestamp >= DateTime.Today);

            if (activityConfigurationToChange.DoneToday != newActivityConfiguration.DoneToday)
            {
                if (!activityConfigurationToChange.DoneToday && newActivityConfiguration.DoneToday)
                {
                    var newEntry = new ActivityEntry()
                    {
                        Activity = _context.Activities.Find(newActivityConfiguration.Activity.ActivityID),
                        Timestamp = DateTime.UtcNow
                    };
                    _context.Users.Find(userID).ActivityEntries.Add(newEntry);

                }
                else if (activityConfigurationToChange.DoneToday && !newActivityConfiguration.DoneToday)
                {
                    var activityToDelete = _context.ActivityEntries.First(ae => ae.Activity.Equals(newActivityConfiguration.Activity) && ae.Timestamp > DateTime.Today);
                    _context.ActivityEntries.Remove(activityToDelete);
                }
            }

            _context.SaveChanges();

            return newActivityConfiguration;
        }
    }
}
