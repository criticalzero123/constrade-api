using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community_post")]
    public class CommunityPost
    {
        [Key]
        [Column("community_post_id")]
        public int CommunityPostId { get; set; }

        [ForeignKey("Community")]
        public int CommunityId { get; set; }
        public Community Community { get; set; }

        [ForeignKey("User")]
        [Column("poster_user_id")]
        public int PosterUserId { get; set; }
        public User User { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("like_count")]
        public int Like { get; set; }
    }
}
