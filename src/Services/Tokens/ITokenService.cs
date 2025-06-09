namespace src.DeviceEmployeeAuthManager.Services.Tokens;

public interface ITokenService
{
    string GenerateToken(string username, string role);
}