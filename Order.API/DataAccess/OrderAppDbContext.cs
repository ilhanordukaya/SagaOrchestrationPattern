using Microsoft.EntityFrameworkCore;
using Order.API.Models;
using System.Collections.Generic;

namespace Order.API.DataAccess
{
	
		public class AppDbContext : DbContext
		{
			public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
			{
			}

			public DbSet<Orderr> Orders { get; set; }
			public DbSet<OrderItem> OrderItems { get; set; }
		}
	
}
