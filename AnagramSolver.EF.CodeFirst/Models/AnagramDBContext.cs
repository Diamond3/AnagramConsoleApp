using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Models;

public class AnagramDbContext: DbContext
{
    public DbSet<CachedWordEntity> CachedWords { get; set; }
    public DbSet<WordEntity> Words { get; set; }
    
    private const string Connection =
        "data source=LT-LIT-SC-0597;initial catalog=AnagramDBCodeFirst;trusted_connection=true;TrustServerCertificate=true";
    
    public AnagramDbContext()
    {
    }
    public AnagramDbContext(DbContextOptions<AnagramDbContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(Connection);
}