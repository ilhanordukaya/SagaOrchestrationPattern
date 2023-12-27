using Microsoft.EntityFrameworkCore;
using Stock.API.Models;
using System.Collections.Generic;

namespace Stock.API.DataAccess
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Stok> Stocks { get; set; }
	}
}
