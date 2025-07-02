using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;
using DeviceEmployeeAuthManager.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace DeviceEmployeeAuthManager.API.Services;

public class PositionService : IPositionService
{
    private readonly DeviceEmployeeDbContext _context;

    public PositionService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetPositionsDto>> GetAllPositions(CancellationToken cancellationToken)
    {
        var roles = await _context.Positions.ToListAsync(cancellationToken);
        return roles.Select(e => new GetPositionsDto
        {
            Id = e.Id,
            Name = $"{e.Name}"
        }).ToList();
    }
}