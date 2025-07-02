using System.Collections.Generic;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;

namespace DeviceEmployeeAuthManager.API.Helpers.Config;

public interface IDeviceValidator
{
    public Task<List<string>> ValidateAdditionalProperties(string deviceJson,
        DeviceEmployeeDbContext context);
}