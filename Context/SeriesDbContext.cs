using Microsoft.EntityFrameworkCore;

namespace DIO.Series.Context
{
    public class SeriesDbContext : DbContext
    {
        public SeriesDbContext(DbContextOptions<SeriesDbContext> options) : base(options)
        {
        }

        public DbSet<Serie> Series { get; set; }
    }
}