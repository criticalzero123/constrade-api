using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MCommunity.MCommunityPost;
using ConstradeApi.Model.MCommunity.MCommunityPostComment;
using ConstradeApi.Model.MUser;
using ConstradeApi.Services.EntityToModel;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Collections;
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
        public async Task<int> CommunityCreatePost(CommunityPostModel info)
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

            return post.CommunityPostId;
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
            IEnumerable<CommunityPost> communityPosts = await _context.CommunityPost.Include(_p => _p.User.Person).Where(_p => _p.CommunityId == communityId).ToListAsync();

            IEnumerable<CommunityPostDetails> posts = communityPosts.GroupJoin(_context.PostComment.ToList(),
                                                                          _cp => _cp.CommunityPostId,
                                                                          _c => _c.CommunityPostId,
                                                                          (_cp, _c) => new { _cp, _c })
                                                                    .SelectMany(result => result._c.DefaultIfEmpty(),
                                                                                (parent, child) => new { parent._cp, child})
                                                                    .GroupBy(result => result._cp,
                                                                             result => result.child,
                                                                             (key, group) => new CommunityPostDetails
                                                                             {
                                                                                 CommunityPost = key.ToModel(),
                                                                                 User = new UserAndPersonModel
                                                                                 {
                                                                                     User = key.User.ToModel(),
                                                                                     Person = key.User.Person.ToModel(),

                                                                                 },
                                                                                 CommentsLength = group.Count(c => c != null),
                                                                             })
                                                                    .OrderByDescending(result => result.CommunityPost.CreatedDate);

            foreach (var post in posts)
            {
                
                    // Log a warning if the comment count doesn't match the expected value
                    Console.WriteLine($"Warning: post {post.CommunityPost.CommunityPostId} has {post.CommentsLength} comments.");
               
            }

            return posts;
        }

        public async Task<bool> DeletePostCommunityById(int postId)
        {
            CommunityPost? post = await _context.CommunityPost.FindAsync(postId);

            if (post == null) return false;

            _context.Remove(post);
            _context.SaveChanges();

            return true;
        }

        // TODO: this will not check if the user is a memeber in the community post
        // Please make a optimizer also here
        public async Task<int> CommentPost(CommunityPostCommentModel info)
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


            return comment.CommunityPostCommentId;
        }

        public async Task<IEnumerable<CommentDetails>> GetCommentByPostId(int id)
        {
            IEnumerable<CommentDetails> comments = await _context.PostComment.Include(_cm => _cm.User.Person)
                                                                             .Where(_p => _p.CommunityPostId == id)
                                                                             .Select(_p => new CommentDetails
                                                                             {
                                                                                 Comment = _p.ToModel(),
                                                                                 UserInfo = new UserAndPersonModel
                                                                                 {
                                                                                     User = _p.User.ToModel(),
                                                                                     Person = _p.User.Person.ToModel(),
                                                                                 }
                                                                             })
                                                                             .ToListAsync();

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

        public async Task<bool> CommunityPostLike(int postId, int userId)
        {
            CommunityPost? post = await _context.CommunityPost.FindAsync(postId);

            if(post == null) return false;

            CommunityPostLike? like = await _context.PostLike.Where(_l => _l.UserId == userId && _l.CommunityPostId == postId).FirstOrDefaultAsync();

            if(like == null)
            {
                post.Like += 1;
                await _context.PostLike.AddAsync(new CommunityPostLike
                {
                    UserId = userId,
                    CommunityPostId = postId,
                });
            }
            else
            {
                post.Like -= 1;
                _context.PostLike.Remove(like);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateComment(CommunityPostCommentModel info)
        {
            CommunityPostComment? comment = await _context.PostComment.FindAsync(info.CommunityPostCommentId);

            if(comment == null) return false;

            comment.Comment = info.Comment;
            comment.DateCommented = DateTime.Now;

            _context.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<CommunityMemberDetails>> GetCommunityMember(int id)
        {
            IEnumerable<CommunityMember> community = await _context.CommunityMember.Include(_cm => _cm.User.Person).Where(_cm => _cm.CommunityId == id).ToListAsync();

            IEnumerable<CommunityMemberDetails> _result = community.Select(_cm => new CommunityMemberDetails
            {
                Member = _cm.ToModel(),
                UserInfo = new UserAndPersonModel
                {
                    User = _cm.User.ToModel(),
                    Person = _cm.User.Person.ToModel()
                }
            });

            return _result;
        }

        public async Task<bool> RemoveMember(int id)
        {
            CommunityMember? exist = await _context.CommunityMember.FindAsync(id);

            if (exist == null) return false;

            Community? community = await _context.Community.FindAsync(exist.CommunityId);

            if (community == null) return false;

            community.TotalMembers -= 1;
            _context.CommunityMember.Remove(exist);

            _context.SaveChanges();

            return true;
        }

        public async Task<IEnumerable<CommunityDetails>> GetCommunityJoined(int userId)
        {
            IEnumerable<Community> communities = await _context.Community.Include(_c => _c.User.Person).ToListAsync();

            var communityMembers = _context.CommunityMember.ToList().Where(_cm => _cm.UserId == userId)
                                                                    .Join(communities,
                                                                          _cm => _cm.CommunityId,
                                                                          _c => _c.CommunityId,
                                                                          (_cm, _c) => new {_cm, _c})
                                                                    .Select(_result => new CommunityDetails
                                                                    {
                                                                        Community = _result._c.ToModel(),
                                                                        Owner = new UserAndPersonModel
                                                                        {
                                                                            Person = _result._c.User.Person.ToModel(),
                                                                            User = _result._c.User.ToModel()
                                                                        },
                                                                    });

            return communityMembers;
        }

        public async Task<IEnumerable<CommunityDetails>> GetPopularCommunity(int userId)
        {
            IEnumerable<Community> communities = await _context.Community.Include(_c => _c.User.Person).ToListAsync();

            var communityMembers = _context.CommunityMember.ToList().Where(_cm => _cm.UserId != userId)
                                                                    .Join(communities,
                                                                          _cm => _cm.CommunityId,
                                                                          _c => _c.CommunityId,
                                                                          (_cm, _c) => new { _cm, _c })
                                                                    .OrderByDescending(_result => _result._c.TotalMembers)
                                                                    .GroupBy(_result => _result._c,
                                                                          _result => _result._cm,
                                                                          (key, value) => new CommunityDetails
                                                                          {
                                                                              Community = key.ToModel(),
                                                                              Owner = new UserAndPersonModel
                                                                              {
                                                                                  Person = key.User.Person.ToModel(),
                                                                                  User = key.User.ToModel()
                                                                              }
                                                                          })
                                                                    .Take(5); 

    

            return communityMembers;
        }
    }
}
