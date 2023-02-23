namespace ConstradeApi.Model.MUserApiKey.Repository
{
    public interface IApiKeyRepository
    {
        Task<ApiKeyModel> CreateApiKeyAsync(int userId);
        Task<ApiKeyModel?> GetApiKeyAsync(string token);
    }
}
