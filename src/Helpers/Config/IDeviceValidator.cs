using src.DeviceEmployeeAuthManager.DAL;

namespace src.DeviceEmployeeAuthManager.Helpers.Config;

public interface IDeviceValidator
{
    public Task<List<string>> ValidateAdditionalProperties(string deviceJson,
        DeviceEmployeeDbContext context);
}