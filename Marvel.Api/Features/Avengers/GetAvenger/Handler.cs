using MinApiLib.Logging;

namespace Marvel.Api.Features.Avengers.GetAvenger;

public record Handler() : GetHandlerAsync<Request>("/avengers/{id}")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
                .ProducesHypermedia<Response>(StatusCodes.Status200OK)
                .Produces<Response>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GetAvenger")
                .WithTags("Avengers");

    protected override async Task<IResult> HandleAsync(Request query,  CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var avenger = await query.Database.Avenger                                     
                                       .FirstOrDefaultAsync(b => b.Id == (int)query.Id, cancellationToken);

        if (avenger is null)
        {
            query.Logger.Information("Not Found");
            return Results.NotFound();
        }

        var resource = (Response)avenger;
        query.Logger.Information($"Avenger is {avenger.Name}");
        return Results.Ok(resource);
    }
}
