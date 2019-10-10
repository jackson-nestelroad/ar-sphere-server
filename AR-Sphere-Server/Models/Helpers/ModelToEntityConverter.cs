using ARSphere.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Models.Helpers
{
	public static class ModelToEntityConverter
	{
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
