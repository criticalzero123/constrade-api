using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUserApiKey.Repository
{
    public class ApiKeyRepository : IApiKeyRepository
    {
        private readonly DataContext _context;

        public ApiKeyRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreateApiKeyAsync(int userId)
        {
            ApiKey session = new ApiKey
            {
               Token = Guid.NewGuid().ToString(),
               UserId = userId,
               DateCreated= DateTime.Now,
               IsActive = true,
            };

            await _context.ApiKey.AddAsync(session);
            await _context.SaveChangesAsync();

            return session.ApiKeyId;
        }

        public async Task<ApiKeyModel?> GetApiKeyByIdAsync(int id)
        {
            var result = await _context.ApiKey.FindAsync(id);

            return result?.ToModel();
        }

        public async Task<ApiKeyModel?> GetApiKeyByTokenAsync(string token)
        {
            ApiKeyModel? apiKey = await _context.ApiKey.Where(x => x.Token == token && x.IsActive)
                .Select(_s => _s.ToModel()).FirstOrDefaultAsync();

            return apiKey;
        }

        public async Task<string?> GetApiKeyByUserIdAsync(int userId)
        {
            ApiKeyModel? apiKey = await _context.ApiKey.Where(x => x.UserId == userId && x.IsActive)
                .Select(_s => _s.ToModel()).FirstOrDefaultAsync();

            if (apiKey == null) return null;

            return apiKey.Token;
        }
    }
}
