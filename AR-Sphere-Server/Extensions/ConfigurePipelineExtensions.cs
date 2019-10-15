using ARSphere.Hubs;
using ARSphere.Persistent;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Extensions
{
	/// <summary>
	/// <para>Gives several configuration methods to further configure the HTTP pipeline.</para>
	/// </summary>
	public static class ConfigurePipelineExtensions
	{
		/// <summary>
		/// <para>Creates a custom routing scheme for the API.</para>
		/// </summary>
		/// <param name="app"></param>
		public static void UseCustomRouting(this IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<MasterHub>("/connect");
				endpoints.MapControllers();
			});
		}

		/// <summary>
		/// <para>Ensures the database seeded by seeding the DatabaseContext service.</para>
		/// <para>The DatabaseContext service is created exclusively for this method, assuring the work is finished
		/// before turning back to the Startup.Configure method.</para>
		/// </summary>
		/// <param name="app"></param>
		/// <param name="autoMigrateDatabase"></param>
		/// <returns>Dummy integer to assure data finishes seeding synchronously.</returns>
		public static int EnsureDatabaseIsSeeded(this IApplicationBuilder app, bool autoMigrateDatabase)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
				if (autoMigrateDatabase)
				{
					context.Database.Migrate();
				}
				return context.EnsureSeedData();
			}
		}

		/// <summary>
		/// <para>Migrates the database on startup.</para>
		/// </summary>
		/// <param name="app"></param>
		/// <returns>Dummy integer to assure data finishes migrating synchronously.</returns>
		public static int MigrateDatabase(this IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
				bool migrationNeeded = Task.Run(() => context.Database.GetPendingMigrationsAsync()).Result.Any();
				if (migrationNeeded)
				{
					context.Database.Migrate();
				}
				return 0;
			}
		}
	}
}	
