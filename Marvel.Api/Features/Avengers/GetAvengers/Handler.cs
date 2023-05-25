using MinApiLib.Logging;

namespace Marvel.Api.Features.Avengers.GetAvengers;

public record Handler() : GetHandlerAsync<Request>("/avengers")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
                .ProducesHypermedia<Response>(StatusCodes.Status200OK)
                .Produces<Response>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .WithName("GetAvengers")
                .WithTags("Avenger");

    protected override async Task<IResult> HandleAsync(Request req, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var total = await req.Database.Avenger.CountAsync(cancellationToken);
        if (total == 0)
        {
            req.Logger.Information("Not content");
            return Results.NoContent();
        }

        var avengers = await req.Database
                                .Avenger
                                .Select(b => new ResponseItem
                                {
                                    Id = b.Id,
                                    Name = b.Name,
                                    Photo= b.Photo                                    
                                })
                                .OrderBy(b => b.Name)
                                .Skip((req.Page - 1) * req.PageSize)
                                .Take(req.PageSize)
                                .ToListAsync(cancellationToken);

        var resourceList = new Response(avengers, total, req.Page, req.PageSize);
        req.Logger.Information($"Avenger Counts {avengers.Count}");
        return Results.Ok(resourceList);
    }
}
