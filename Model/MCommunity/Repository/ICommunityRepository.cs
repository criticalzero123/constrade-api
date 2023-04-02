using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity.MCommunityJoinRequest;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MCommunity.MCommunityPost;
using ConstradeApi.Model.MCommunity.MCommunityPostComment;

namespace ConstradeApi.Model.MCommunity.Repository
{
    public interface ICommunityRepository
    {
        Task<CommunityResponse> CreateCommunity(CommunityModel info);
        Task<IEnumerable<CommunityModel>> GetCommunityByOwnerId(int userId);
        Task<IEnumerable<CommunityItem>> GetCommunityJoined(int userId);
        Task<IEnumerable<CommunityItem>> GetCommunities(int userId);
        Task<CommunityDetails?> GetCommunity(int id);

        /// <summary>
        /// GET: 5 of the community order by total members that is not joined by the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of community</returns>
        Task<IEnumerable<CommunityItem>> GetPopularCommunity(int userId);
        Task<bool> DeleteCommunity(int id, int userId);
        Task<CommunityJoinResponse> JoinCommunity(int id, int userId);
        Task<IEnumerable<CommunityMemberDetails>> GetCommunityMember(int id);
        Task<bool> RemoveMember(int id);
        Task<bool> UpdateCommunity(CommunityModel info);
        Task<int> CommunityCreatePost(CommunityPostModel info);
        Task<bool> UpdatePost(CommunityPostModel info);
        Task<bool> CommunityPostLike(int postId, int userId);
        Task<IEnumerable<CommunityPostDetails>> GetAllCommunityPost(int communityId, int userId);
        Task<bool> DeletePostCommunityById(int postId);
        Task<int> CommentPost(CommunityPostCommentModel info);
        Task<IEnumerable<CommentDetails>> GetCommentByPostId(int id);
        Task<bool> DeleteCommentPost(int id);
        Task<bool> UpdateComment(CommunityPostCommentModel info);
        Task<IEnumerable<CommunityJoinModel>> GetMemberRequests(int communityId);
        Task<bool> AcceptMemberRequest(int id);
        Task<bool> RejectMemberRequest(int id);
        Task<IEnumerable<CommunityItem>> GetSearchCommunity(string text, int userId);
    }
}
