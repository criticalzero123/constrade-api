using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MUser;
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

        public async Task<bool> DeleteCommunity(int id, int userId)
        {
            Community? community = await _context.Community.Where(_c => _c.CommunityId == id 
                                                                && _c.OwnerUserId == userId)
                                                           .FirstOrDefaultAsync();

            if (community == null) return false;

            _context.Community.Remove(community);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunities()
        {
            return await _context.Community.Select(_c => _c.ToModel()).ToListAsync();
        }

        public async Task<CommunityDetails?> GetCommunity(int id)
        {
            Community? model = await _context.Community.Include(_c => _c.User).Include(_c => _c.User.Person).Where(_c => _c.CommunityId == id).FirstOrDefaultAsync();

            if (model == null) return null;

            IEnumerable<CommunityMemberModel> members = await _context.CommunityMember.Where(_c => _c.CommunityId == model.CommunityId).Select(_c => _c.ToModel()).ToListAsync();

            return new CommunityDetails
            {
                Community = model.ToModel(),
                Owner = new UserAndPersonModel
                {
                    User = model.User.ToModel(),
                    Person = model.User.Person.ToModel(),
                },
                Members = members
            };
        }

        public async Task<IEnumerable<CommunityModel>> GetCommunityByOwnerId(int userId)
        {
            IEnumerable<CommunityModel> communityList = await _context.Community.Where(_c => _c.OwnerUserId == userId).Select(_c => _c.ToModel()).ToListAsync();

            return communityList;
        }

        public async Task<CommunityJoinResponse> JoinCommunity(int id, int userId)
        {
            Community? community = await _context.Community.FindAsync(id);

            if (community == null) return CommunityJoinResponse.Failed;

            if (community.Visibility.Equals("private"))
            {
                CommunityJoin _info = new CommunityJoin
                {
                    CommunityId = community.CommunityId,
                    UserId = userId,
                    Status = CommunityJoinResponse.Pending,
                    DateRequested = DateTime.Now,
                };
                await _context.CommunityJoin.AddAsync(_info);

                await _context.SaveChangesAsync();

                return CommunityJoinResponse.Pending;
            } 
            else
            {
                CommunityMember _info = new CommunityMember
                {
                    CommunityId = community.CommunityId,
                    UserId = userId,
                    Role = CommunityRole.Member,
                    MemberSince = DateTime.Now,
                };

                community.TotalMembers += 1;
                await _context.CommunityMember.AddAsync(_info);

                return CommunityJoinResponse.Approved;
            }
        }

        public async Task<bool> UpdateCommunity(CommunityModel info)
        {
            Community? _community = await _context.Community.FindAsync(info.CommunityId);

            if (_community == null) return false;
            if (_community.OwnerUserId != info.OwnerUserId) return false;

            _community.Description = info.Description;
            _community.ImageUrl = info.ImageUrl;
            _community.Visibility = info.Visibility;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
