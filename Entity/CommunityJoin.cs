using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community_join")]
    public class CommunityJoin
    {
        [Key]
        [Column("community_join_id")]
        public int CommunityJoinRequestId { get; set; }

        [Column("community_id")]
        public int CommunityId { get; set; }
        public Community Community { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("status")]
        public CommunityJoinResponse Status { get; set; }

        [Column("date_requested")]
        public DateTime DateRequested { get; set; }
    }
}
