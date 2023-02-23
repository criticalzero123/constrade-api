namespace ConstradeApi.Model.MUserApiKey.Repository
{
    public interface IApiKeyRepository
    {
        Task<int> CreateApiKeyAsync(int userId);
        Task<ApiKeyModel?> GetApiKeyByTokenAsync(string token);
        Task<string?> GetApiKeyByUserIdAsync(int userId);
        Task<ApiKeyModel?> GetApiKeyByIdAsync(int id);
    }
}
