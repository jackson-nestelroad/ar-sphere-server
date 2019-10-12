using ARSphere.Configuration;
using ARSphere.Context;
using ARSphere.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Extensions
{
	/// <summary>
	/// <para>Gives several configuration methods to further configure the services container</para>
	/// </summary>
	public static class ConfigureContainerExtensions
	{
		private static string ConnectionString => new DatabaseConfiguration().GetDatabaseConnectionString();
		
		/// <summary>
		/// <para>Creates and injects the DatabaseContext service with its required options.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void AddDbContext(this IServiceCollection services)
		{
			services.AddDbContext<DatabaseContext>(
				options => options.UseSqlServer(ConnectionString, 
					x => x.UseNetTopologySuite()
				)
			);
		}

		/// <summary>
		/// <para>Adds all transient services to be used by the controllers and hubs.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void AddTransientServices(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
		}

		/// <summary>
		/// <para>Disables automatic model state validation.</para>
		/// <para>Use the <c>[ValidateModel]</c> filter instead when model validation is desired.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void DisableModelStateValidation(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});
		}
	}
}
