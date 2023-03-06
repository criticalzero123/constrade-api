using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MCommunity
{
    public class CommunityDetails
    {
        public CommunityModel Community { get; set; }  
        public UserAndPersonModel Owner { get; set; }
        public IEnumerable<CommunityMemberModel> Members { get; set; }
    }
}
