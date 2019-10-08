using ARSphere.Context.Helpers;
using ARSphere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARSphere.Context
{
	public class DatabaseContext : DbContext, IDatabaseContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
		public DatabaseContext() { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddPrimaryKeys();
		}

		public DbSet<User> Users { get; set; }
	}
}
