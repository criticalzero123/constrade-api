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

        public async Task<ApiKeyModel> CreateApiKeyAsync(int userId)
        {
            ApiKey session = new ApiKey
            {
               Token = Guid.NewGuid().ToString(),
               UserId = userId,
               DateCreated= DateTime.UtcNow,
               IsActive = true,
            };

            await _context.ApiKey.AddAsync(session);
            await _context.SaveChangesAsync();

            return session.ToModel();
        }

        public async Task<ApiKeyModel?> GetApiKeyAsync(string token)
        {
            ApiKeyModel? session = await _context.ApiKey.Where(x => x.Token == token && x.IsActive)
                .Select(_s => _s.ToModel()).FirstOrDefaultAsync();

            return session;
        }
    }
}
