namespace Hackathon.Service.Systems;

public interface IService
{
    Dictionary<string, Dictionary<string, string>> GetEnums();
    Task<Response.HealthResponse> GetHealth(DateTime startupTime);
    Response.VersionResponse GetVersion(string environmentName);
    Task<Response.UploadFileResponse> UploadFile(Request.FileUploadRequest request);
}
