using ARSphere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Context.Helpers
{
	public static class ModelBuilderExtensions
	{
		public static void AddPrimaryKeys(this ModelBuilder builder)
		{
			builder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
		}
	}
}
