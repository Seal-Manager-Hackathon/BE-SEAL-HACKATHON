using System.Linq;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Entity;
using Hackathon.Repository.Enum;
using Hackathon.Service.Admin.Request;
using Hackathon.Service.Admin.Response;
using Hackathon.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Admin;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<BasePaginationResponse> GetAllUsers(PaginationRequest paginationRequest)
    {
        var q = _dbContext.Users.AsNoTracking();

        var totalCount = await q.CountAsync();

        paginationRequest.PageIndex = paginationRequest.PageIndex <= 0 ? 1 : paginationRequest.PageIndex;
        paginationRequest.PageSize = paginationRequest.PageSize <= 0 ? 10 : System.Math.Min(paginationRequest.PageSize, 100);

        var items = await BuildUserQuery(q, paginationRequest);
        return ApiResponseFactory.BasePagination(items, paginationRequest.PageIndex, paginationRequest.PageSize, totalCount);
    }

    public async Task<BasePaginationResponse> SearchUsers(GetUsersQuery query)
    {
        var q = _dbContext.Users.AsNoTracking();

        if (query.IsDisable.HasValue)
        {
            q = q.Where(x => x.IsDisable == query.IsDisable.Value);
        }

        if (query.IsVerified.HasValue)
        {
            q = q.Where(x => x.IsVerified == query.IsVerified.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.MailSearch))
        {
            var normalized = query.MailSearch.Trim().ToLower();
            q = q.Where(x => x.Email.ToLower().Contains(normalized));
        }

        if (query.IdSearch.HasValue)
        {
            q = q.Where(x => x.Id == query.IdSearch.Value);
        }

        if (query.Role.HasValue)
        {
            q = q.Where(x => x.Role == query.Role.Value);
        }

        if (!string.IsNullOrWhiteSpace(query.StudentIdSearch))
        {
            var normalizedStudentId = query.StudentIdSearch.Trim().ToLower();
            q = q.Where(x => x.StudentId.ToLower().Contains(normalizedStudentId));
        }

        var totalCount = await q.CountAsync();

        query.Pagination.PageIndex = query.Pagination.PageIndex <= 0 ? 1 : query.Pagination.PageIndex;
        query.Pagination.PageSize = query.Pagination.PageSize <= 0 ? 10 : System.Math.Min(query.Pagination.PageSize, 100);

        var items = await BuildUserQuery(q, query.Pagination);
        return ApiResponseFactory.BasePagination(items, query.Pagination.PageIndex, query.Pagination.PageSize, totalCount);
    }

    private async Task<List<AdminUserResponse>> BuildUserQuery(IQueryable<Hackathon.Repository.Entity.Users> q, PaginationRequest pagination)
    {
        return await q
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .Select(x => new AdminUserResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
                StudentId = x.StudentId,
                College = x.College,
                Role = x.Role,
                Status = x.Status,
                IsVerified = x.IsVerified,
                IsDisable = x.IsDisable,
                CreatedAt = x.CreatedAt
            })
            .ToListAsync();
    }
}
