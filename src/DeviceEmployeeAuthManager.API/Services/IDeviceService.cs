using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DTO;

namespace DeviceEmployeeAuthManager.API.Services;

public interface IDeviceService
{
    public Task<List<GetDevicesDto>> GetAllDevices(CancellationToken cancellationToken);

    public Task<List<GetDeviceTypesDto>> GetDeviceTypesDto(CancellationToken cancellationToken);

    public Task<GetDeviceDto?> GetDeviceById(int id, CancellationToken cancellationToken);

    public Task<bool> CreateDevice(CreateDeviceDto createDeviceDto, CancellationToken cancellationToken);

    public Task<bool> UpdateDevice(int id, UpdateDeviceDto updateDeviceDto, CancellationToken cancellationToken);

    public Task<bool> DeleteDevice(int id, CancellationToken cancellationToken);

    public Task<List<int>> GetDeviceIdsByEmployeeId(int id, CancellationToken cancellationToken);
}