using Habitica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica_API.Repositories
{
    internal interface IActivityRepository
    {
        public Activity CreateActivity(Activity activity);

        public Activity UpdateActivity(Activity activity);

        public bool DeleteActivity(string activityId);
    }
}
