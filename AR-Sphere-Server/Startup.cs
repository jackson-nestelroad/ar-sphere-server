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
using ARSphere.Context;
using Microsoft.EntityFrameworkCore;
using ARSphere.Extensions;

namespace ARSphere
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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

			services.AddMvc();
			services.AddSignalR();

			services.AddDbContext();
			services.AddTransientServices();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
