using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Configuration
{
	public class DatabaseConfiguration : ConfigurationBase
	{
		private string connectionStringKey = "DatabaseContext";
		public string GetDatabaseConnectionString()
		{
			return GetConfiguration().GetConnectionString(connectionStringKey);
		}
	}
}
