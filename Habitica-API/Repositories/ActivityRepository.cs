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

        public List<ActivityConfiguration> GetUserActivities(int userID)
        {
            var user = _context.Users.Find(userID);
            var activitiesDoneToday = _context.Users.Find(userID).ActivityEntries.Where(ua => ua.Timestamp >= DateTime.Today).Select(a => a.Activity);
            user.ActivityData.ForEach(ad => ad.DoneToday = activitiesDoneToday.Contains(ad.Activity));
            return user.ActivityData;
        }

        public ActivityConfiguration UpdateActivity(ActivityConfiguration newActivityConfiguration, int userID)
        {
            var activityConfigurationToChange = _context.ActivityConfiguration.First(ac => ac == newActivityConfiguration);
            activityConfigurationToChange.DoneToday = _context.Users.Find(userID).ActivityEntries.Any(ae => ae.Activity.Equals(activityConfigurationToChange.Activity) && ae.Timestamp >= DateTime.Today);

            if (activityConfigurationToChange.DoneToday != newActivityConfiguration.DoneToday)
            {
                var user = _context.Users.Find(userID);
                if (!activityConfigurationToChange.DoneToday && newActivityConfiguration.DoneToday)
                {
                    var newEntry = new ActivityEntry()
                    {
                        Activity = _context.Activities.Find(newActivityConfiguration.Activity.ActivityID),
                        Timestamp = DateTime.UtcNow
                    };

                    user.ActivityEntries.Add(newEntry);
                    user.Experience += (int)newActivityConfiguration.Difficulty;

                }
                else if (activityConfigurationToChange.DoneToday && !newActivityConfiguration.DoneToday)
                {
                    var activityToDelete = user.ActivityEntries.First(ae => ae.Activity.ActivityID.Equals(newActivityConfiguration.Activity.ActivityID) && ae.Timestamp > DateTime.Today);
                    user.ActivityEntries.Remove(activityToDelete);
                    user.Experience -= (int)newActivityConfiguration.Difficulty;
                    user.Experience = user.Experience < 0 ? 0 : user.Experience;
                    _context.ActivityEntries.Remove(activityToDelete);
                }
            }

            _context.SaveChanges();

            return newActivityConfiguration;
        }

        public List<ActivityEntry> GetActivityHistory(int activityID, int userID)
        {
            return _context.Users.Find(userID).ActivityEntries.Where(ae => ae.Activity.ActivityID.Equals(activityID)).ToList();
        }
    }
}
