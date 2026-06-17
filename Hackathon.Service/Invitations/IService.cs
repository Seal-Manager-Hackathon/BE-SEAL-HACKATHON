using System.Threading.Tasks;
using Hackathon.Service.Models;

namespace Hackathon.Service.Invitations;

public interface IService
{
    Task<BasePaginationResponse> GetMyInvitations(int pageIndex, int pageSize);
}
