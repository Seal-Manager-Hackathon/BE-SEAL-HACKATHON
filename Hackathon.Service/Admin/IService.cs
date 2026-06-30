using System.Threading.Tasks;
using Hackathon.Repository.Enum;
using Hackathon.Service.Admin.Request;
using Hackathon.Service.Models;

namespace Hackathon.Service.Admin;

public interface IService
{
    Task<BasePaginationResponse> GetAllUsers(RoleEnum? role, PaginationRequest paginationRequest);
    Task<BasePaginationResponse> SearchUsers(GetUsersQuery query);
}
