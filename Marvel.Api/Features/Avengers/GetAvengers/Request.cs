namespace Marvel.Api.Features.Avengers.GetAvengers;

public record struct Request(
 [FromServices] AvengerDbContext Database,
 [FromServices] ILogger<Request> Logger,
 [FromQuery] int Page = 1,
 [FromQuery] int PageSize = 10
);

