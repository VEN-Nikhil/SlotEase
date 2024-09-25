namespace SlotEase.Domain;

public class ConfigSettings
{

    public string SqlServerConnectionString { get; set; }
    public string CorsOrigin { get; set; }
    public bool UseInMemoryCache { get; set; }
    public string OriginHeader { get; set; }

    public JwtSettings Jwt { get; set; }


}

public class JwtSettings
{
    public string? ApiName { get; set; }
    public string ApiSecret { get; set; } = null!;
    public bool RequireHttpsMetadata { get; set; }
    public string KeepMeSignedInTime { get; set; } = null!;
    public string TokenExpiryInMinutes { get; set; } = null!;
}

