using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Fokus.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Activity> Activities { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(GetActivity());
            base.OnModelCreating(modelBuilder);
        }

        private List<Activity> GetActivity()
        {
            return new List<Activity>
            {
                new Activity {Id = 1, Created = DateTime.Parse("08/09/2021 16:00"), Name = "Gym", Duration = 30, Calories = 225},
                new Activity {Id = 2, Created = DateTime.Parse("08/09/2021 17:00"), Name = "Gym", Duration = 35, Calories = 225},
                new Activity {Id = 3, Created = DateTime.Parse("08/09/2021 18:00"), Name = "Gym", Duration = 90, Calories = 225},
            };
        }
    }
}