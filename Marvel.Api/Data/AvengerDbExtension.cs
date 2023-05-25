namespace Marvel.Api.Data;

public static class AvengerDbExtensions
{
    public static IServiceCollection AddAvengerDbContext(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<AvengerDbContext>(options =>
            options.UseSqlite("Data Source=beers.db"));
        return serviceCollection;
    }

    public static void SeedAvengerDbData(this IHost app)
    {
        var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

        using var scope = scopedFactory.CreateScope();
        var db = scope.ServiceProvider.GetService<AvengerDbContext>();
        db.Database.EnsureCreated();
        if (db.Avenger.Any())
        {
            return;
        }
        db.Avenger.AddRange(Seed.Avengers);
        db.SaveChanges();
    }
}