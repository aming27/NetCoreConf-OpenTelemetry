using Microsoft.EntityFrameworkCore.Design;

namespace Marvel.Api.Data;

public class AvengerDbContextFactory : IDesignTimeDbContextFactory<AvengerDbContext>
{
    public AvengerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AvengerDbContext>();
        optionsBuilder.UseSqlite("Data Source=avenger.db");

        return new AvengerDbContext(optionsBuilder.Options);
    }
}