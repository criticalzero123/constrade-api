namespace ConstradeApi.Model.MUserAuthorize.Repository
{
    public interface IUserAuthorizeRepository
    {
        Task<UserAuthorizeModel> CreateApiKeyAsync(int userId);
        Task<UserAuthorizeModel?> GetApiKeyAsync(string token);
    }
}
