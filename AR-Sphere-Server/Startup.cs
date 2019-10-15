using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ARSphere.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.SignalR;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ARSphere.Extensions;
using ARSphere.DAL;

namespace ARSphere
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

		/// <summary>
		/// <para>This method is called at runtime. Use this method to add services to the container.</para>
		/// </summary>
		/// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
			//services.AddAuthentication()
			//	.AddPolicyScheme


			//services.AddAuthentication(options =>
			//{
			//	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			//	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			//}).AddJwtBearer(options =>
			//{
			//	options.TokenValidationParameters = new TokenValidationParameters
			//	{
			//		LifetimeValidator = (before, expires, token, param) =>
			//		{
			//			return expires > DateTime.UtcNow;
			//		},
			//		ValidateAudience = false,
			//		ValidateIssuer = false,
			//		ValidateActor = false,
			//		ValidateLifetime = true
			//	};

			//	options.Events = new JwtBearerEvents
			//	{
			//		OnMessageReceived = context =>
			//		{
			//			var accessToken = context.Request.Query["access_token"];

			//			var path = context.HttpContext.Request.Path;
			//			if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/connect"))
			//			{
			//				context.Token = accessToken;
			//			}
			//			return Task.CompletedTask;
			//		}
			//	};
			//});

			// Add MVC for REST API
			services.AddMvc();

			// Add SignalR for streaming data
			services.AddSignalR(options =>
			{
				options.EnableDetailedErrors = true;
			});

			// Call container configuration extensions
			services.AddDbContext(Configuration);
			services.AddServiceOfBaseClass<BaseService>(new[] { typeof(Startup).Assembly });
			services.DisableModelStateValidation();
		}

		/// <summary>
		/// <para>This method is called at runtime. Use this method to configure the HTTP request pipeline.</para>
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			app.MigrateDatabase();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.EnsureDatabaseIsSeeded(false);
			}

			app.UseHttpsRedirection();
			app.UseCustomRouting();
		}
    }
}
