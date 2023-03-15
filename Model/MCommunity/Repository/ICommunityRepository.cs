using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MCommunity.MCommunityPost;
using ConstradeApi.Model.MCommunity.MCommunityPostComment;

namespace ConstradeApi.Model.MCommunity.Repository
{
    public interface ICommunityRepository
    {
        Task<CommunityResponse> CreateCommunity(CommunityModel info);
        Task<IEnumerable<CommunityModel>> GetCommunityByOwnerId(int userId);
        Task<IEnumerable<CommunityDetails>> GetCommunityJoined(int userId);
        Task<IEnumerable<CommunityModel>> GetCommunities();
        Task<CommunityDetails?> GetCommunity(int id);
        Task<bool> DeleteCommunity(int id, int userId);
        Task<CommunityJoinResponse> JoinCommunity(int id, int userId);
        Task<IEnumerable<CommunityMemberDetails>> GetCommunityMember(int id);
        Task<bool> RemoveMember(int id);
        Task<bool> UpdateCommunity(CommunityModel info);
        Task<CommunityPostDetails> CommunityCreatePost(CommunityPostModel info);
        Task<bool> CommunityPostLike(int postId, int userId);
        Task<IEnumerable<CommunityPostDetails>> GetAllCommunityPost(int communityId);
        Task<bool> DeletePostCommunityById(int postId);
        Task<CommunityPostCommentModel> CommentPost(CommunityPostCommentModel info);
        Task<IEnumerable<CommunityPostCommentModel>> GetCommentByPostId(int id);
        Task<bool> DeleteCommentPost(int id);
        Task<CommunityPostCommentModel?> UpdateComment(CommunityPostCommentModel info);
    }
}
