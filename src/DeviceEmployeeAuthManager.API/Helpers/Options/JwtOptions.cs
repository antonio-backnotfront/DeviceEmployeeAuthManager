namespace DeviceEmployeeAuthManager.API.Helpers.Options;

public class JwtOptions
{
    // who issues

    public required string Issuer { get; set; }

    // for who
    public required string Audience { get; set; }
    public required string Key { get; set; }
    public required int ValidInMinutes { get; set; }
}