using System.Collections.Generic;
using Hackathon.Repository.Enum;

namespace Hackathon.Service.Roles;

public interface IService
{
    Task<List<RoleResponse>> GetRoles();
    Task<List<EventRoleResponse>> GetEventRoles();
}
