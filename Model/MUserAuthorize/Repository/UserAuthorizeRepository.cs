using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUserAuthorize.Repository
{
    public class UserAuthorizeRepository : IUserAuthorizeRepository
    {
        private readonly DataContext _context;

        public UserAuthorizeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserAuthorizeModel> CreateApiKeyAsync(int userId)
        {
            ApiKey? exist = await _context.ApiKey.Where(_u => _u.UserId == userId).FirstOrDefaultAsync();

            if (exist != null)
            {
                exist.Created = DateTime.Now;
                exist.Expires = DateTime.Now.AddHours(12);
                await _context.SaveChangesAsync();

                return exist.ToModel();
            }
            
            ApiKey session = new ApiKey
            {
               Token = Guid.NewGuid().ToString(),
               UserId = userId,
               Created= DateTime.Now,
               Expires = DateTime.Now.AddHours(12),
            };
            

            await _context.ApiKey.AddAsync(session);
            await _context.SaveChangesAsync();

            return session.ToModel();
        }

        public async Task<UserAuthorizeModel?> GetApiKeyAsync(string token)
        {
            UserAuthorizeModel? session = await _context.ApiKey.Where(x => x.Token == token && x.Expires > DateTime.Now)
                .Select(_s => _s.ToModel()).FirstOrDefaultAsync();

            return session;
        }
    }
}
