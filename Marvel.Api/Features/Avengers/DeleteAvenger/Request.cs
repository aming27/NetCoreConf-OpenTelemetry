namespace Marvel.Api.Features.Avengers.DeleteAvenger;

public record struct Request(
    [FromServices] AvengerDbContext Database,
    [FromRoute] HashedId Id
);
