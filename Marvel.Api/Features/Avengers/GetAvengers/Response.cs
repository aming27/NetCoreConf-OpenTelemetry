namespace Marvel.Api.Features.Avengers.GetAvengers;

public class Response : PagedList<ResponseItem>
{
    public Response() : base()
    {
    }

    public Response(IEnumerable<ResponseItem> items, int total, int page, int pageSize) : base(items, total, page, pageSize)
    {
    }
}

public class ResponseItem
{
    public HashedId Id { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }

}

public class ResponseHypermediaProvider : HypermediaProvider<Response>
{
    protected override IEnumerable<HypermediaLink> GetLinksFor(Response @object)
    {
        yield return new HypermediaLink("self", $"/avengers?page={@object.Page}&pageSize={@object.PageSize}", "GET");
        if (@object.Page > 1)
        {
            yield return new HypermediaLink("previous", $"/avengers?page={@object.Page - 1}&pageSize={@object.PageSize}", "GET");
        }
        if (@object.Page < @object.TotalPages)
        {
            yield return new HypermediaLink("next", $"/avengers?page={@object.Page + 1}&pageSize={@object.PageSize}", "GET");
        }

        yield return new HypermediaLink("create", "/avengers", "POST");
    }
}
