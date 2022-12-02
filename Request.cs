public class Request
{
    public int Id { get; init; }
    public Guid RequestGuid { get; init; } = Guid.NewGuid();
    public string? RequestName { get; init; }
    public string? SiteName { get; init; }
    public string? CompanyName { get; init; }
    public string? Mail { get; init; }
    public byte[] Archive { get; init; } = Array.Empty<byte>();
    public DateTime RequestDate { get; init; } = DateTime.UtcNow;
    public string UserId { get; init; } = "anonymous";
    public virtual ICollection<ModuleHost> Hosts { get; init; } = new List<ModuleHost>();
}
