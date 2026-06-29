using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hackathon.Repository;
using Hackathon.Repository.Enum;
using Microsoft.EntityFrameworkCore;

namespace Hackathon.Service.Roles;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<RoleResponse>> GetRoles()
    {
        var roles = new List<RoleResponse>();
        foreach (var name in Enum.GetNames<RoleEnum>())
        {
            var value = (int)Enum.Parse<RoleEnum>(name);
            roles.Add(new RoleResponse
            {
                Id = value,
                Name = name,
                DisplayName = name
            });
        }
        return roles;
    }

    public async Task<List<EventRoleResponse>> GetEventRoles()
    {
        var eventRoles = await _dbContext.EventRoles
            .AsNoTracking()
            .ToListAsync();

        return eventRoles.Select(x => new EventRoleResponse
        {
            RoleId = x.Id,
            Id = (int)x.Name,
            Name = x.Name.ToString(),
            DisplayName = x.Name.ToString()
        }).ToList();
    }
}
