using ConstradeApi.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community_member")]
    public class CommunityMember
    {
        [Key]
        [Column("community_member_id")]
        public int CommunityMemberId { get; set; }

        [ForeignKey("Community")]
        [Column("community_id")]
        public int CommunityId { get; set; }
        public Community Community { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }  

        [Column("role")]
        public CommunityRole Role { get; set; }

        [Column("member_since")]
        public DateTime MemberSince { get; set; }
    }
}
