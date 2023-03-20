using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MCommunity.MCommunityPostComment
{
    public class CommentDetails
    {
        public CommunityPostCommentModel Comment { get; set; }
        public UserAndPersonModel UserInfo { get; set; }
    }
}
