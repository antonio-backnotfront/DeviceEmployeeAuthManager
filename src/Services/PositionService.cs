using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public class PositionService : IPositionService
{
    private DeviceEmployeeDbContext _context;

    public PositionService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetPositionsDto>> GetAllPositions(CancellationToken cancellationToken)
    {
        var roles = await _context.Positions.ToListAsync(cancellationToken);
        return roles.Select(e => new GetPositionsDto()
        {
            Id = e.Id,
            Name = $"{e.Name}"
        }).ToList();
    }
}