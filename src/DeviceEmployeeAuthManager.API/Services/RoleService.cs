using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeviceEmployeeAuthManager.API.DAL;
using DeviceEmployeeAuthManager.API.DTO;
using Microsoft.EntityFrameworkCore;

namespace DeviceEmployeeAuthManager.API.Services;

public class RoleService : IRoleService
{
    private readonly DeviceEmployeeDbContext _context;

    public RoleService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<List<GetRoleDto>> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _context.Roles.ToListAsync(cancellationToken);
        return roles.Select(e => new GetRoleDto
        {
            Id = e.Id,
            Name = $"{e.Name}"
        }).ToList();
    }
}