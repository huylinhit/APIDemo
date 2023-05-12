using System;
using APIDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.DbContexts
{
	public class CityInfoContext : DbContext
	{
		public DbSet<City> Cities { get; set; } = null!;
		public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Ho Chi Minh")
                {
                    Id = 1,
                    Description = "Big City"
                },
                new City("Ha Noi")
                {
                    Id = 2,
                    Description = "City"
                },
                new City("Da Lat")
                {
                    Id = 3,
                    Description = "Tour City"
                }
            );

            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                    new PointOfInterest("Central Parl")
                    {
                        Id = 1,
                        CityId = 1,
                        Description = "a"
                    },
                    new PointOfInterest("Empire State Building")
                    {
                        Id = 2,
                        CityId = 1,
                        Description = "a"

                    },
                    new PointOfInterest("Vung Tau")
                    {
                        Id = 3,
                        CityId = 2,
                        Description = "a"

                    },
                    new PointOfInterest("Ba Ria")
                    {
                        Id = 4,
                        CityId = 2,
                        Description = "a"

                    },
                    new PointOfInterest("Dong Thap")
                    {
                        Id = 5,
                        CityId = 3,
                        Description = "a"

                    },
                    new PointOfInterest("Hoc Mon")
                    {
                        Id = 6,
                        CityId = 3,
                        Description = "Nothing Here"
                    }
                );


            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("ConnectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

