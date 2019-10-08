using ARSphere.Configuration;
using ARSphere.Context;
using ARSphere.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Extensions
{
	public static class ConfigureContainerExtensions
	{
		private static string DbConnectionString => new DatabaseConfiguration().GetDatabaseConnectionString();

		public static void AddDbContext(this IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>(options =>
				options.UseSqlServer(DbConnectionString));
		}

		public static void AddTransientServices(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
		}
	}
}
