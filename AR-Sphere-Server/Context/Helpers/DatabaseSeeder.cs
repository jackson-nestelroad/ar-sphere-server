using ARSphere.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Context.Helpers
{
	/// <summary>
	/// <para>Provides methods to seed the database with JSON data in the <c>./SeedData</c> folder.</para>
	/// </summary>
	public class DatabaseSeeder
	{
		private readonly DatabaseContext _context;

		public DatabaseSeeder(DatabaseContext context)
		{
			_context = context;
		}

		public async Task<int> SeedUserEntitiesFromJson()
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Context", "Helpers", "SeedData", "UserSeedData.json");
			if(!File.Exists(filePath))
			{
				throw new FileNotFoundException($"The file {filePath} does not exist.");
			}

			var dataSet = File.ReadAllText(filePath);
			var seedData = JsonConvert.DeserializeObject<List<User>>(dataSet);

			foreach (var seedUser in seedData)
			{
				_context.Users.Add(seedUser);
				await _context.SaveChangesAsync();
			}

			return default;
		}
	}
}
