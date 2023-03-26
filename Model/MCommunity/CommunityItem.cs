namespace ConstradeApi.Model.MCommunity
{
    public class CommunityItem
    {
        public CommunityModel Community { get; set; }
        public string OwnerImage { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        public bool IsJoined { get; set; }
    }
}
