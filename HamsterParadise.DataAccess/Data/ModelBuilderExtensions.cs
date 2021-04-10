using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    /// <summary>
    /// A number of custom-made ModelBuilder extensions for 
    /// configuring the database and doing database-seeding.
    /// </summary>
    public static class ModelBuilderExtensions
    {
        public static void DatabaseSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.ActivityCreate();
            modelBuilder.CageCreate();
            modelBuilder.ExerciseAreaCreate();
            modelBuilder.OwnerCreate();
            modelBuilder.HamsterCreate();
        }

        #region DatabaseSeeding
        public static void ActivityCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(new Activity { Id = 1, ActivityName = "Arrived" });
            modelBuilder.Entity<Activity>().HasData(new Activity { Id = 2, ActivityName = "Exercise" });
            modelBuilder.Entity<Activity>().HasData(new Activity { Id = 3, ActivityName = "Cage" });
            modelBuilder.Entity<Activity>().HasData(new Activity { Id = 4, ActivityName = "Departure" });
        }
        public static void CageCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 1 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 2 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 3 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 4 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 5 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 6 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 7 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 8 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 9 });
            modelBuilder.Entity<Cage>().HasData(new Cage { Id = 10 });
        }
        public static void ExerciseAreaCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseArea>().HasData(new ExerciseArea { Id = 1 });
        }
        public static void HamsterCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 1, Name = "Nisse", Age = 2, IsFemale = false, OwnerId = 5, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 2, Name = "Junior", Age = 1, IsFemale = false, OwnerId = 1, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 3, Name = "Rufus", Age = 3, IsFemale = false, OwnerId = 10, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 4, Name = "Lilleman", Age = 1, IsFemale = false, OwnerId = 15, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 5, Name = "Vilma", Age = 2, IsFemale = true, OwnerId = 2, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 6, Name = "Greta", Age = 2, IsFemale = true, OwnerId = 9, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 7, Name = "Brutus", Age = 3, IsFemale = false, OwnerId = 12, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 8, Name = "Pelle", Age = 1, IsFemale = false, OwnerId = 3, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 9, Name = "Buttons", Age = 1, IsFemale = false, OwnerId = 21, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 10, Name = "Apple", Age = 2, IsFemale = true, OwnerId = 16, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 11, Name = "Fluffy", Age = 1, IsFemale = true, OwnerId = 13, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 12, Name = "Einstein", Age = 3, IsFemale = false, OwnerId = 11, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 13, Name = "Bella", Age = 2, IsFemale = true, OwnerId = 9, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 14, Name = "Taco", Age = 3, IsFemale = false, OwnerId = 7, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 15, Name = "Klopapier", Age = 2, IsFemale = false, OwnerId = 20, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 16, Name = "Anna Book", Age = 2, IsFemale = true, OwnerId = 4, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 17, Name = "Mindy", Age = 1, IsFemale = true, OwnerId = 6, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 18, Name = "Dota", Age = 2, IsFemale = true, OwnerId = 3, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 19, Name = "Candy", Age = 1, IsFemale = true, OwnerId = 16, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 20, Name = "Bubba", Age = 3, IsFemale = false, OwnerId = 19, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 21, Name = "Elvis", Age = 2, IsFemale = false, OwnerId = 8, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 22, Name = "Alexandra", Age = 3, IsFemale = true, OwnerId = 17, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 23, Name = "Lulu", Age = 3, IsFemale = true, OwnerId = 18, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 24, Name = "Miss Vegan", Age = 2, IsFemale = true, OwnerId = 5, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 25, Name = "Falukorv", Age = 1, IsFemale = false, OwnerId = 1, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 26, Name = "Blondie", Age = 2, IsFemale = true, OwnerId = 4, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 27, Name = "Hasse", Age = 2, IsFemale = false, OwnerId = 14, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 28, Name = "Richard Gere", Age = 3, IsFemale = false, OwnerId = 8, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 29, Name = "Gullis", Age = 2, IsFemale = true, OwnerId = 20, CageId = null, ExerciseAreaId = null });
            modelBuilder.Entity<Hamster>().HasData(new Hamster { Id = 30, Name = "Summer", Age = 1, IsFemale = true, OwnerId = 12, CageId = null, ExerciseAreaId = null });
        }
        public static void OwnerCreate(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 1, Name = "Fredrik Parkell", Email = @"fredrikparkell5@gmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 2, Name = "Stefan Trenh", Email = @"steffeboi2021@protonmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 3, Name = "Nils Brufors", Email = @"ninaspelardota2@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 4, Name = "Johan Nilsson", Email = @"annab00kfan111@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 5, Name = "David Lindgren Kamali", Email = @"commiev3ganf0l1fe@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 6, Name = "Einar Olafsson", Email = @"iceland4l1fe@protonmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 7, Name = "Emile Nestor", Email = @"ringhalsen@yandex.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 8, Name = "Olof Svahn", Email = @"333333svanen333333@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 9, Name = "Julia Nilsson", Email = @"jjjjjjnnnn123@yandex.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 10, Name = "Johannes Posse", Email = @"jp0racing@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 11, Name = "Andreas Lind", Email = @"llll1ebro@hotmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 12, Name = "Ludvig Sköld", Email = @"shieldman9000@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 13, Name = "Madelene Sjösten", Email = @"dlk1337@outlook.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 14, Name = "Andrea Envall", Email = @"innebandyproffset100@protonmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 15, Name = "Louis Headlam", Email = @"falkenbergaren1@yandex.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 16, Name = "Albin Ahmetaj", Email = @"falkenbergaren2@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 17, Name = "Mattis Kindblom", Email = @"mrlaholm@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 18, Name = "Robin Tranberg", Email = @"robinlovesvarberg@yopmail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 19, Name = "Simon Hörnfalk", Email = @"5muneagle@mail.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 20, Name = "Carl Fredrik Ahl", Email = @"ccccffff00@outlook.com" });
            modelBuilder.Entity<Owner>().HasData(new Owner { Id = 21, Name = "Paul Tannenberg", Email = @"p9hhjds33@protonmail.com" });
        }
        #endregion

        #region ModelConfiguring
        public static void ActivityModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>()
                        .Property(a => a.ActivityName)
                        .HasMaxLength(25)
                        .IsRequired();
        }
        public static void ActivityLogModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivityLog>()
                        .HasOne(s => s.Simulation)
                        .WithMany(al => al.ActivityLogs)
                        .IsRequired()
                        .HasForeignKey(s => s.SimulationId);
            modelBuilder.Entity<ActivityLog>()
                        .HasOne(a => a.Activity)
                        .WithMany(al => al.ActivityLogs)
                        .IsRequired()
                        .HasForeignKey(a => a.ActivityId);
            modelBuilder.Entity<ActivityLog>()
                        .HasOne(h => h.Hamster)
                        .WithMany(al => al.ActivityLogs)
                        .IsRequired()
                        .HasForeignKey(h => h.HamsterId);
            modelBuilder.Entity<ActivityLog>(t => t.Property(t => t.TimeStamp).HasColumnType("datetime2(0)"));
        }
        public static void CageModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cage>(e => e.HasCheckConstraint("CK_Cage_CageSize", "[CageSize] >= 0 AND [CageSize] < 4"));
            modelBuilder.Entity<Cage>()
                        .Property(c => c.CageSize)
                        .HasDefaultValue(0);
        }
        public static void ExerciseAreaModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseArea>(e => e.HasCheckConstraint("CK_ExerciseArea_CageSize", "[CageSize] >= 0 AND [CageSize] < 7"));
            modelBuilder.Entity<ExerciseArea>()
                        .Property(c => c.CageSize)
                        .HasDefaultValue(0);
        }
        public static void HamsterModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hamster>()
                        .Property(n => n.Name)
                        .HasMaxLength(25)
                        .IsRequired();
            modelBuilder.Entity<Hamster>(t => t.Property(t => t.CheckedInTime).HasColumnType("datetime2(0)"));
            modelBuilder.Entity<Hamster>(t => t.Property(t => t.LastExerciseTime).HasColumnType("datetime2(0)"));
            modelBuilder.Entity<Hamster>()
                        .HasOne(o => o.Owner)
                        .WithMany(h => h.Hamsters)
                        .IsRequired()
                        .HasForeignKey(o => o.OwnerId);
            modelBuilder.Entity<Hamster>()
                        .HasOne(c => c.Cage)
                        .WithMany(h => h.Hamsters)
                        .HasForeignKey(c => c.CageId);
            modelBuilder.Entity<Hamster>()
                        .HasOne(e => e.ExerciseArea)
                        .WithMany(h => h.Hamsters)
                        .HasForeignKey(e => e.ExerciseAreaId);
        }
        public static void OwnerModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                        .Property(n => n.Name)
                        .HasMaxLength(45)
                        .IsRequired();
            modelBuilder.Entity<Owner>()
                        .Property(e => e.Email)
                        .HasMaxLength(65)
                        .IsRequired();
        }
        public static void SimulationModelConfiguring(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Simulation>()
                        .Property(n => n.Name)
                        .HasMaxLength(45)
                        .IsRequired();
        }
        #endregion
    }
}
