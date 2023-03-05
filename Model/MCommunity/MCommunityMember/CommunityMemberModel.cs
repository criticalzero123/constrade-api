using ConstradeApi.Entity;
using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MCommunity.MCommunityMember
{
    public class CommunityMemberModel
    {
        public int CommunityMemberId { get; set; }
        public int CommunityId { get; set; }
        public int UserId { get; set; }
        public CommunityRole Role { get; set; }
        public DateTime MemberSince { get; set; }
    }
}
