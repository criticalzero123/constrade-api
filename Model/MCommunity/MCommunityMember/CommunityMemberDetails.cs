using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MCommunity.MCommunityMember
{
    public class CommunityMemberDetails
    {
        public CommunityMemberModel Member { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserImageUrl { get; set; } = string.Empty;
        
    }
}
