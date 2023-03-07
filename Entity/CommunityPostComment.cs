using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community_post_comment")]
    public class CommunityPostComment
    {
        [Key]
        [Column("community_post_comment_id")]
        public int CommunityPostCommentId { get; set; }

        [ForeignKey("Post")]
        [Column("community_post_id")]
        public int CommunityPostId { get; set; }
        public CommunityPost Post { get; set; }

        [ForeignKey("User")]
        [Column("commented_by_user")]
        public int CommentedByUser { get; set; }
        public User User { get; set; }

        [Column("comment")]
        public string Comment { get; set; } = string.Empty;
        [Column("date_commented")]
        public DateTime DateCommented { get; set; } = DateTime.Now;
    }
}
