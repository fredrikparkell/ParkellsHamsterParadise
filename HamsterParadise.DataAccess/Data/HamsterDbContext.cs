using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    /// <summary>
    /// Custom-made DbContext for the database used in this application.
    /// </summary>
    public class HamsterDbContext : DbContext
    {
        /// <summary>
        /// Don't forget to change the connectionstring to make 
        /// sure it is configuredand will work on your computer!
        /// </summary>
        private static string ConnString = @"Server=DESKTOP-C9DV26N\SQLEXPRESS;Database=advFredrikParkell;
                                            Trusted_Connection=True;MultipleActiveResultSets=True;";

        #region DbSet Propertys
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<Cage> Cages { get; set; }
        public DbSet<ExerciseArea> ExerciseAreas { get; set; }
        public DbSet<Hamster> Hamsters { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        #endregion

        #region OnConfiguring-method
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString).UseLazyLoadingProxies();
        }
        #endregion

        #region OnModelCreating-method
        /// <summary>
        /// Uses the ModelBuilder-class to configure the database aswell as
        /// doing database seeding, meaning data goes with the database from
        /// the "get-go" everytime it is created.
        /// 
        /// Here I am calling ModelBuilder-extension-methods that I have made
        /// using a custom-made ModelBuilderExtensions-class. This is both to
        /// make the code cleaner in the OnModelCreating-method but also to
        /// split up the different configurations based on the specific Entity.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ActivityModelConfiguring(); // Activity
            modelBuilder.ActivityLogModelConfiguring(); // ActivityLog
            modelBuilder.CageModelConfiguring(); // Cage
            modelBuilder.ExerciseAreaModelConfiguring(); // ExerciseArea
            modelBuilder.HamsterModelConfiguring(); // Hamster
            modelBuilder.OwnerModelConfiguring(); // Owner
            modelBuilder.SimulationModelConfiguring(); // Simulation

            modelBuilder.DatabaseSeed(); // Database Seeding; adds the initial data
        }
        #endregion
    }
}
