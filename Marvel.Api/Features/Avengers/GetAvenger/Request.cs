namespace Marvel.Api.Features.Avengers.GetAvenger;

public record struct Request(
 [FromServices] AvengerDbContext Database,
 [FromServices] ILogger<Request> Logger,
 [FromRoute] HashedId Id
);
