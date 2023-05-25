namespace Marvel.Api.Features.Avengers.CreateAvenger;

public class Response
{
    public HashedId Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }

    public static implicit operator Response(Avenger avenger)
        => new()
        {
            Id = avenger.Id,
            Name = avenger.Name,
            Photo = avenger.Photo
        };
}



public class ResponseHypermediaProvider : HypermediaProvider<Response>
{
    protected override IEnumerable<HypermediaLink> GetLinksFor(Response @object)
    {
        yield return new HypermediaLink("self", "/avengers/" + @object.Id, "GET");
        yield return new HypermediaLink("update", "/avengers/" + @object.Id, "PUT");
        yield return new HypermediaLink("delete", "/avengers/" + @object.Id, "DELETE");
        yield return new HypermediaLink("avengers", "/avengers", "GET");
    }
}
