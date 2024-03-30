using Microsoft.EntityFrameworkCore;
using Spg.ContentTeil.DomainModel.Model;

namespace Spg.ContentTeil.Infrastructure;
public class YourDbContext : DbContext
{
    public YourDbContext(DbContextOptions<YourDbContext> options) : base(options)
    {
    }

    protected YourDbContext()
        : this(new DbContextOptions<YourDbContext>())
    { }

    public DbSet<Content> Contents { get; set; } = null!;


}