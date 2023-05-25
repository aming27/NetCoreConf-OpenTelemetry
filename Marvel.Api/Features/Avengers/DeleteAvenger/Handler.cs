namespace Marvel.Api.Features.Avengers.DeleteAvenger;

public record Handler() : DeleteHandlerAsync<Request>("/avengers/{id}")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DeleteAvenger")
                .WithTags("Avenger");

    protected override async Task<IResult> HandleAsync(Request req, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var beer = await req.Database.Avenger.FindAsync(new object[] { (int)req.Id }, cancellationToken);
        if (beer is null)
        {
            return Results.NotFound();
        }

        req.Database.Avenger.Remove(beer);
        await req.Database.SaveChangesAsync(cancellationToken);

        return Results.NoContent();
    }
}