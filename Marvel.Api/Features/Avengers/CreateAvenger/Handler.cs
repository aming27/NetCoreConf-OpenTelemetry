namespace Marvel.Api.Features.Avengers.CreateAvenger
{
    public record Handler() : PostHandlerAsync<Request>("/avengers")
    {
        protected override RouteHandlerBuilder Configure(RouteHandlerBuilder builder)
            => builder
                    .ProducesHypermedia<Response>(StatusCodes.Status201Created)
                    .Produces<Response>(StatusCodes.Status201Created)
                    .Produces(StatusCodes.Status204NoContent)
                    .Produces(StatusCodes.Status400BadRequest)
                    .WithName("CreateAvenger")
                    .WithTags("Avengers")
                    .WithValidation();

        protected override async Task<IResult> HandleAsync(Request req, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var avenger = new Avenger
            {
                Name = req.Body.Name,
                //Photo= req.Body.Photo
            };

            req.Database.Avenger.Add(avenger);
            await req.Database.SaveChangesAsync(cancellationToken);

            var resource = (Response)avenger;
            return Results.CreatedAtRoute("GetAvenger", new { id = resource.Id.ToString() }, resource);
        }
    }
}
