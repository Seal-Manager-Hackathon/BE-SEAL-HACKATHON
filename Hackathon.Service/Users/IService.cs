namespace Hackathon.Service.Users;

public interface IService
{
    Task<Response.ProfileResponse> GetProfile();
    Task<Response.ProfileResponse> UpdateProfile(Request.UpdateProfileRequest request);
}
