namespace Marvel.Api.Data;

public class AvengerDbContext : DbContext
{
    public DbSet<Avenger> Avenger { get; set; }


    public AvengerDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
