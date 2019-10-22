using ARSphere.DAL;
using ARSphere.Middleware.Validation;
using ARSphere.Persistent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ARSphere.Extensions
{
	/// <summary>
	/// <para>Gives several configuration methods to further configure the services container</para>
	/// </summary>
	public static class ConfigureContainerExtensions
	{		
		/// <summary>
		/// <para>Creates and injects the DatabaseContext service with its required options.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<DatabaseContext>(
				options => options.UseSqlServer(configuration.GetConnectionString("DatabaseContext"),
					x => x.UseNetTopologySuite()
				)
			);
		}

		/// <summary>
		/// <para>Adds all services to the container that derives a given base class.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void AddServiceOfBaseClass<T>(
			this IServiceCollection services, 
			Assembly[] assemblies, 
			ServiceLifetime lifetime = ServiceLifetime.Transient)
		{
			var typesFromAssemblies = assemblies.SelectMany(a => a.DefinedTypes.Where(x => x.BaseType == typeof(T)));
			foreach(var type in typesFromAssemblies)
			{
				var interfaceType = type.GetInterfaces().Where(i => i.Name == "I" + type.Name);
				services.Add(new ServiceDescriptor(interfaceType.Any() ? interfaceType.First() : type, type, lifetime));
			}
		}

		/// <summary>
		/// <para>Adds any other transient services to the container the application needs.</para>
		/// </summary>
		/// <param name="services"></param>
		public static void AddTransientServices(this IServiceCollection services)
		{
			services.AddTransient<IValidationService, ValidationService>();
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
