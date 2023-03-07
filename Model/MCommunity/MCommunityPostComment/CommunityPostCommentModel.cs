using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MCommunity.MCommunityPostComment
{
    public class CommunityPostCommentModel
    {
        public int CommunityPostCommentId { get; set; }
        public int CommunityPostId { get; set; }
        public int CommentedByUser { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime DateCommented { get; set; }
    }
}
