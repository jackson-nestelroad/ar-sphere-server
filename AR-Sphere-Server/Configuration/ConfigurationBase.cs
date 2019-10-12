using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Configuration
{
	/// <summary>
	/// <para>Static class to provide access to the app's configuration file.</para>
	/// </summary>
	public abstract class ConfigurationBase
	{
		protected IConfigurationRoot GetConfiguration()
		{
			return new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();
		}
	}
}
