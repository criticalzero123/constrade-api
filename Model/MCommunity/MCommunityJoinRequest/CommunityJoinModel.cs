

using ConstradeApi.Enums;

namespace ConstradeApi.Model.MCommunity.MCommunityJoinRequest
{
    public class CommunityJoinModel
    {
        public int CommunityJoinRequestId { get; set; }
        public int CommunityId { get; set; }
        public int UserId { get; set; }
        public CommunityJoinResponse Status { get; set; }
        public DateTime DateRequested { get; set; }
    }
}
