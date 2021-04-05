using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class HamsterDbContext : DbContext
    {
        private static string ConnString = @"Server=DESKTOP-C9DV26N\SQLEXPRESS;Database=advFredrikParkell;
                                            Trusted_Connection=True;MultipleActiveResultSets=True;";

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<ExerciseArea> ExerciseAreas { get; set; }
        public DbSet<Hamster> Hamsters { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Simulation> Simulations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ActivityModelConfiguring(); // Activity
            modelBuilder.ActivityLogModelConfiguring(); // ActivityLog
            modelBuilder.CageModelConfiguring(); // Cage
            modelBuilder.ExerciseAreaModelConfiguring(); // ExerciseArea
            modelBuilder.HamsterModelConfiguring(); // Hamster
            modelBuilder.OwnerModelConfiguring(); // Owner
            modelBuilder.SimulationModelConfiguring(); // Simulation

            modelBuilder.DatabaseSeed();
        }
    }
}
