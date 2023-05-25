namespace Marvel.Api.Features.Avengers.CreateAvenger;

public record struct Request(
 [FromServices] AvengerDbContext Database,
 [FromBody] RequestBody Body
);

public class RequestBody
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Name { get; set; }

    public string Photo { get; set; }

}
