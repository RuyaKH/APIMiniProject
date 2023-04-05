namespace NorthwindEmployeeAPI.HATEOAS;

public class LinkResourceBase
{
    public LinkResourceBase()
    {

    }

    public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
}
