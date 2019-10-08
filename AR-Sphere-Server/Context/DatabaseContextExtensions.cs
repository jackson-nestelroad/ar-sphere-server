using ARSphere.Context.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Context
{
	public static class DatabaseContextExtensions
	{
		public static int EnsureSeedData(this DatabaseContext context)
		{
			int sum = 0;

			var dbSeeder = new DatabaseSeeder(context);
			if(!context.Users.Any())
			{
				sum += dbSeeder.SeedUserEntitiesFromJson().Result;
			}

			return sum;
		}
	}
}
