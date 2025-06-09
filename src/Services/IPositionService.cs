using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public interface IPositionService
{
    public Task<List<GetPositionsDto>> GetAllPositions(CancellationToken cancellationToken);
}