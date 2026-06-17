using Hackathon.Repository;

namespace Hackathon.Service.Rounds;

public class Service : IService
{
    private readonly AppDbContext _dbContext;

    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Response.RoundResponse>> GetRounds(Guid? eventId, bool? isDisable)
    {
        throw new NotImplementedException();
    }
}
