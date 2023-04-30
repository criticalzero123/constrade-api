using ConstradeApi.Model.MCommunity.MCommunityPostComment;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MCommunity.MCommunityPost
{
    public class CommunityPostDetails
    {
        public CommunityPostModel CommunityPost { get; set;}
        public UserAndPersonModel User { get; set;}
        public int CommentsLength { get; set;}
        public bool IsLiked { get; set;}
        public bool IsMember { get; set;}
    }
}
