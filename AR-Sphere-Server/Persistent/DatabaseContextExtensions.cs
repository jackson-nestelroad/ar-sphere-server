using ARSphere.Persistent.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Persistent
{
    /// <summary>
    /// <para>Static class to provide extra functionality to our DatabaseContext.</para>
    /// </summary>
    public static class DatabaseContextExtensions
    {
        /// <summary>
        /// <para>Seeds the database with data if the tables are empty.</para>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int EnsureSeedData(this DatabaseContext context)
        {
            int sum = 0;

            var dbSeeder = new DatabaseSeeder(context);
            if (!context.Users.Any())
            {
                sum += dbSeeder.SeedUserEntitiesFromJson().Result;
            }

            return sum;
        }
    }
}
