using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("follow")]
    public class Follow
    {
        [Key]
        [Column("follow_id")]
        public int FollowId { get; set; }

        /// <summary>
        /// This is for the one who got follow
        /// </summary>
        [ForeignKey("User1")]
        [Column("follow_by_user_id")]
        [Required]
        public int FollowByUserId { get; set; }
        public User User1 { get; set; }

        /// <summary>
        /// This User is the follower
        /// </summary>
        [ForeignKey("User2")]
        [Column("followed_by_user_id")]
        [Required]
        public int FollowedByUserId { get; set; }
        public User User2 { get; set; }

        [Column("date_followed")]
        public DateTime DateFollowed{ get; set; }

    }
}
