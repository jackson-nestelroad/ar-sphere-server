using ARSphere.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARSphere.Persistent.Helpers
{
    /// <summary>
    /// <para>Static class to provide extensions to the ModelBuilder class.</para>
    /// </summary>
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// <para>Configures entities to map to their primary key.</para>
        /// </summary>
        /// <param name="builder"></param>
        public static void AddPrimaryKeys(this ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users").HasKey(u => u.Id);
            builder.Entity<Anchor>().ToTable("Anchors").HasKey(a => a.Id);
            builder.Entity<ARModel>().ToTable("ARModels").HasKey(m => m.Id);
            builder.Entity<Sponsor>().ToTable("Sponsors").HasKey(s => s.Id);
            builder.Entity<Promotion>().ToTable("Promotions").HasKey(p => p.Id);
        }
    }
}
