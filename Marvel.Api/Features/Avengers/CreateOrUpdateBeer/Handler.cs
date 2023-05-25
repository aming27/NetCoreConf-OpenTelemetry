
namespace Marvel.Api.Features.Avengers.CreateOrUpdateBeer;

public record Handler() : PutHandlerAsync<Request>("/avengers/{id}")
{
    protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
        => builder
                .ProducesHypermedia<Response>(StatusCodes.Status201Created)
                .Produces<Response>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("CreateOrUpdateAvenger")
                .WithTags("Avengers")
                .WithValidation();

    protected override async Task<IResult> HandleAsync(Request req, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        

        var avenger = await req.Database.Avenger.FindAsync(new object[] { (int)req.Id }, cancellationToken);
        if (avenger is null)
        {
            avenger = new Avenger
            {
                Id = (int)req.Id
            };

            req.Database.Avenger.Add(avenger);
        }

        avenger.Name = req.Body.Name;
        avenger.Photo = req.Body.Photo;
     

        await req.Database.SaveChangesAsync(cancellationToken);

        var resource = (Response)avenger;
        return Results.Ok(resource);
    }
}
