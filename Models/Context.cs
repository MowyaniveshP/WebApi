using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
	public class Context : DbContext
	{

		public Context(DbContextOptions<Context> options) : base(options)
		{
		}

		public DbSet<Event> Events { get; set; }
		public DbSet<Location> Locations { get; set; }
	}
}
