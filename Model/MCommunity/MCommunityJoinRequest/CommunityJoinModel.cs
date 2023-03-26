

using ConstradeApi.Enums;

namespace ConstradeApi.Model.MCommunity.MCommunityJoinRequest
{
    public class CommunityJoinModel
    {
        public int CommunityJoinRequestId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserImageUrl { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime DateRequested { get; set; }
    }
}
