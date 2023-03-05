using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MCommunity.Repository
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly DataContext _context;

        public CommunityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<CommunityResponse> CreateCommunity(CommunityModel info)
        {
            User? owner = await _context.Users.FindAsync(info.OwnerUserId);

            if (owner == null) return CommunityResponse.Failed;
            if (owner.UserType != "verified") return CommunityResponse.NotVerified;

            Community community = new Community
            {
                Name = info.Name,
                Description = info.Description,
                OwnerUserId= owner.UserId,
                Visibility= info.Visibility,
                ImageUrl= info.ImageUrl,
                DateCreated = DateTime.Now,
                TotalMembers = 1
            };

            await _context.Community.AddAsync(community);
            await _context.SaveChangesAsync();

            CommunityMember member = new CommunityMember
            {
                UserId = owner.UserId,
                CommunityId = community.CommunityId,
                Role = CommunityRole.Owner,
                MemberSince = DateTime.Now
            };

            await _context.CommunityMember.AddAsync(member);
            await _context.SaveChangesAsync();

            return CommunityResponse.Success;
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunities()
        {
            return await _context.Community.Select(_c => _c.ToModel()).ToListAsync();
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunityByOwnerId(int userId)
        {
            IEnumerable<CommunityModel> communityList = await _context.Community.Where(_c => _c.OwnerUserId == userId).Select(_c => _c.ToModel()).ToListAsync();

            return communityList;
        }
    }
}
