using System.Text.Json.Nodes;
using src.DeviceEmployeeAuthManager.Models;
using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public interface IDeviceService
{
    public Task<List<GetDevicesDto>> GetAllDevices(CancellationToken cancellationToken);
    
    public Task<GetDeviceDto?> GetDeviceById(int id, CancellationToken cancellationToken);
    
    public Task<bool> CreateDevice(CreateDeviceDto createDeviceDto, CancellationToken cancellationToken);
    
    public Task<bool> UpdateDevice(int id, UpdateDeviceDto updateDeviceDto, CancellationToken cancellationToken);
    
    public Task<bool> DeleteDevice(int id, CancellationToken cancellationToken);

    public Task<List<int>> GetDeviceIdsByEmployeeId(int id, CancellationToken cancellationToken);
}