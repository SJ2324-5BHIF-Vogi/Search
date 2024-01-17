using Microsoft.EntityFrameworkCore;
using Spg.Search.DomainModel.Model;

namespace Spg.Search.Infrastructure;
public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

    protected YourDbContext()
        : this(new DbContextOptions<YourDbContext>())
    { }

    public DbSet<SearchHistory> SearchHistories { get; set; } = null!;


}