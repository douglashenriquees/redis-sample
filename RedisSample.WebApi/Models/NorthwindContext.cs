using Microsoft.EntityFrameworkCore;

namespace RedisSample.WebApi.Models;

public class NorthwindContext : DbContext
{
    public DbSet<Order>? Orders { get; set; }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }
}