using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MCommunity.MCommunityPost;
using ConstradeApi.Model.MCommunity.MCommunityPostComment;
using ConstradeApi.Model.MUser;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

                await _context.SaveChangesAsync();


                return CommunityJoinResponse.Approved;
            }
        }

        // TODO: this will not check if the user exist in the community 
        // Please do a checker here
        public async Task<CommunityPostDetails> CommunityCreatePost(CommunityPostModel info)
        {
            CommunityPost post = new CommunityPost
            {
                CommunityId = info.CommunityId,
                PosterUserId = info.PosterUserId,
                Description = info.Description,
                CreatedDate = info.CreatedDate
            };

            await _context.AddAsync(post);
            await _context.SaveChangesAsync();

            CommunityPost _post = await _context.CommunityPost.Include(_p => _p.User).Where(_p => _p.CommunityPostId == post.CommunityPostId).FirstAsync();


            return new CommunityPostDetails
            {
                CommunityPost = _post.ToModel(),
                User = _post.User.ToModel()
            };
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

        public async Task<IEnumerable<CommunityPostDetails>> GetAllCommunityPost(int communityId)
        {
            IEnumerable<CommunityPost> communityPosts = await _context.CommunityPost.Include(_p => _p.User).Where(_p => _p.CommunityId == communityId).ToListAsync();

            IEnumerable<CommunityPostDetails> posts = communityPosts.Select(_p => new CommunityPostDetails
            {
                CommunityPost = _p.ToModel(),
                User = _p.User.ToModel()
            });

            return posts;
        }
        public async Task<bool> DeletePostCommunityById(int postId, int userId)
        {
            CommunityPost? post = await _context.CommunityPost.FindAsync(postId);

            if (post == null) return false;
            if (post.PosterUserId != userId) return false;

            _context.Remove(post);
            _context.SaveChanges();

            return true;
        }

        // TODO: this will not check if the user is a memeber in the community post
        // Please make a optimizer also here
        public async Task<CommunityPostCommentModel> CommentPost(CommunityPostCommentModel info)
        {
            CommunityPostComment comment = new CommunityPostComment
            {
                CommunityPostId = info.CommunityPostId,
                CommentedByUser = info.CommentedByUser,
                Comment = info.Comment,
                DateCommented = info.DateCommented,
            };

            await _context.PostComment.AddAsync(comment);
            await _context.SaveChangesAsync();

            CommunityPostComment? _comment = await _context.PostComment.FindAsync(comment.CommunityPostCommentId);

            return _comment.ToModel();
        }

        public async Task<IEnumerable<CommunityPostCommentModel>> GetCommentByPostId(int id)
        {
            IEnumerable<CommunityPostCommentModel> comments = await _context.PostComment.Where(_p => _p.CommunityPostId == id).Select(_p => _p.ToModel()).ToListAsync();

            return comments;
        }

        public async Task<bool> DeleteCommentPost(int id)
        {
            CommunityPostComment? comment = await _context.PostComment.FindAsync(id);

            if(comment == null) return false;   

            _context.Remove(comment);
            _context.SaveChanges();

            return true;
        }
    }
}
