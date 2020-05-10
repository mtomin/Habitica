using Habitica;
using Habitica_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habitica_API.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Index for unique constraint on username.
            modelBuilder.Entity<RegisteredUser>(ru=>ru.HasIndex(e=>e.Username).IsUnique());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Activity> Activities { get; set; }

        public virtual DbSet<ActivityEntry> ActivityEntries { get; set; }

        public virtual DbSet<ActivityConfiguration> ActivityConfiguration { get; set; }

        public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }
    }
}
