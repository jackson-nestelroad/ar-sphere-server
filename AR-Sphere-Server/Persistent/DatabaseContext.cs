using ARSphere.Entities;
using ARSphere.Persistent.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ARSphere.Persistent
{
	/// <summary>
	/// <para>Context which interacts directly with the MSSQL database.</para>
	/// <para>Only to be used directly by the API services.</para>
	/// </summary>
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.AddPrimaryKeys();
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Anchor> Anchors { get; set; }
		public DbSet<ARModel> ARModels { get; set; }
		public DbSet<Sponsor> Sponsors { get; set; }
		public DbSet<Promotion> Promotions { get; set; }
	}
}
