using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community_post_like")]
    public class CommunityPostLike
    {
        [Key]
        public int CommunityPostLikeId { get; set; }
        
        [ForeignKey("Post")]
        public int CommunityPostId { get; set; }   
        public CommunityPost Post { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
