using ConstradeApi.Entity;
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

                return new UserAuthorizeModel
                {
                    Token = exist.Token,
                    UserId = exist.UserId,
                    Created = exist.Created,
                    Expires = exist.Expires,
                };
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

            return new UserAuthorizeModel
            {
                Token = session.Token,
                UserId = session.UserId,
                Created = session.Created,
                Expires = session.Expires,
            };
        }

        public async Task<UserAuthorizeModel?> GetApiKeyAsync(string token)
        {
            UserAuthorizeModel? session = await _context.ApiKey.Where(x => x.Token == token && x.Expires > DateTime.Now)
                .Select(_s => new UserAuthorizeModel()
                {
                    ApiKeyId = _s.ApiKeyId,
                    Token = _s.Token,
                    UserId = _s.UserId,
                    Created = _s.Created,
                    Expires = _s.Expires,
                }).FirstOrDefaultAsync();

            return session;
        }
    }
}
