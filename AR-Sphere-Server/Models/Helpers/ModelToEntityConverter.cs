using ARSphere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models.Helpers
{
	/// <summary>
	/// <para>Static class to convert Models (fed directly to the API) to Entities for storage in the <c>DatabaseContext</c>.</para>
	/// </summary>
	public static class ModelToEntityConverter
	{
		/// <summary>
		/// <para>Converts model data for a User into an entity for the database.</para>
		/// </summary>
		/// <param name="userModel"></param>
		/// <returns></returns>
		public static User ToEntity(this UserModel userModel)
		{
			return new User
			{
				Username = userModel.Username,
				Email = userModel.Password,
				Password = "HASHED",
				RegisteredAt = DateTime.UtcNow
			};
		}
	}
}
