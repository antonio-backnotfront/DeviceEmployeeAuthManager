using Microsoft.EntityFrameworkCore;
using src.DeviceEmployeeAuthManager.DAL;
using src.DeviceEmployeeAuthManager.DTO;

namespace src.DeviceEmployeeAuthManager.Services;

public class RoleService : IRoleService
{
    private DeviceEmployeeDbContext _context;
    public RoleService(DeviceEmployeeDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetRoleDto>> GetAllRoles(CancellationToken cancellationToken)
    {
        try
        {
            var roles = await _context.Roles.ToListAsync(cancellationToken);
            return roles.Select(e => new GetRoleDto()
            {
                Id = e.Id,
                Name = $"{e.Name}"
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error while getting all roles", ex);
        }
    }
}