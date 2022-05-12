using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst.Models;

public class AnagramDBContext: DbContext
{
    public DbSet<CachedWordEntity> CachedWords { get; set; }
    public DbSet<WordEntity> Words { get; set; }

    private const string ProviderName = "System.Data.SqlClient";
    private const string Connection =
        "data source=LT-LIT-SC-0597;initial catalog=AnagramDBCodeFirst;trusted_connection=true;TrustServerCertificate=true";
    
    public AnagramDBContext()
    {
    }
    public AnagramDBContext(DbContextOptions<AnagramDBContext> options)
        : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(Connection);
}