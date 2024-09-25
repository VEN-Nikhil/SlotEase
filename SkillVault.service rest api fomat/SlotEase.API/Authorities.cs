namespace SlotEase.API;

public class Authorities
{
    public string? AuthenticationScheme { get; set; }
    public string? Authority { get; set; }
    public string? IssuerUri { get; set; }
    public string? ApiSecret { get; set; }
    public string? ApiName { get; set; }
    public bool RequireHttpsMetadata { get; set; }
}