

namespace ConstradeApi.Model.MUser
{
    public class UserFollowModel
    {
        public int FollowId { get; set; }
        /// <summary>
        /// This is for the one who got follow
        /// </summary>
        public int FollowByUserId { get; set; }
        /// <summary>
        /// This User is the follower
        /// </summary>
        public int FollowedByUserId { get; set; }
        public DateTime DateFollowed { get; set; }
    }
}
