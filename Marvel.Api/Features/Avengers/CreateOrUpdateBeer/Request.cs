namespace Marvel.Api.Features.Avengers.CreateOrUpdateBeer;


public record struct Request(
    [FromServices] AvengerDbContext Database,
    [FromBody] RequestBody Body,
    [FromRoute] HashedId Id
);

public class RequestBody
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }
    public string Photo { get; set; }
}
