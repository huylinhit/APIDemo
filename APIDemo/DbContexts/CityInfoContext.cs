using System;
using APIDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIDemo.DbContexts
{
	public class CityInfoContext : DbContext
	{
		public DbSet<City> City { get; set; } = null!;
		public DbSet<PointOfInterest> PointOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("ConnectionString");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}

