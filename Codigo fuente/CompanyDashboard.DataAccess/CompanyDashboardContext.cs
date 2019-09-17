using System;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;

namespace CompanyDashboard.DataAccess
{
    public class CompanyDashboardContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Node> Node { get; set; }
        public DbSet<UserIndicator> UsersIndicators { get; set; }


        public CompanyDashboardContext(DbContextOptions options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AreaUser>().HasKey(au => new { au.AreaID, au.UserID });

            modelBuilder.Entity<AreaUser>()
            .HasOne(au => au.area)
            .WithMany(a => a.AreaUsers)
            .HasForeignKey(au => au.AreaID);

            modelBuilder.Entity<AreaUser>()
            .HasOne(au => au.user)
            .WithMany(e => e.AreaUsers)
            .HasForeignKey(au => au.UserID);
            
            modelBuilder.Entity<Node>()
            .HasOne(node => node.Right);
        }

    }
}
