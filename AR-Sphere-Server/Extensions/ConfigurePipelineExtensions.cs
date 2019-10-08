using ARSphere.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Extensions
{
	public static class ConfigurePipelineExtensions
	{
		public static void UseCustomRouting(this IApplicationBuilder app)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				// endpoints.MapHub<ConnectHub>("/connect");
				endpoints.MapControllers();
			});
		}

		public static int EnsureDatabaseIsSeeded(this IApplicationBuilder app, bool autoMigrateDatabase)
		{
			using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetRequiredService<DatabaseContext>();
				if(autoMigrateDatabase)
				{
					context.Database.Migrate();
				}
				return context.EnsureSeedData();
			}
		}
	}
}
