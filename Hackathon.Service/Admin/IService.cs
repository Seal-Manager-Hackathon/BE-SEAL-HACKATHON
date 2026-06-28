using System.Threading.Tasks;
using Hackathon.Service.Admin.Request;
using Hackathon.Service.Models;

namespace Hackathon.Service.Admin;

public interface IService
{
    Task<BasePaginationResponse> GetAllUsers(PaginationRequest paginationRequest);
    Task<BasePaginationResponse> SearchUsers(GetUsersQuery query);
}
