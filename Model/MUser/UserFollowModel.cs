

namespace ConstradeApi.Model.MUser
{
    public class UserFollowModel
    {
        public int FollowId { get; set; }
        public int FollowByUserId { get; set; }
        public int FollowedByUserId { get; set; }
        public DateTime DateFollowed { get; set; }
    }
}
