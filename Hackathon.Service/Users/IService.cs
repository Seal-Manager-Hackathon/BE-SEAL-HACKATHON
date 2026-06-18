namespace Hackathon.Service.Users;

public interface IService
{
    Task<Reponse.UserProfileDetailResponse> GetProfileUser();
}