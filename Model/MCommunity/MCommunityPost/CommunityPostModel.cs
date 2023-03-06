namespace ConstradeApi.Model.MCommunity.MCommunityPost
{
    public class CommunityPostModel
    {
        public int CommunityPostId { get; set; }
        public int CommunityId { get; set; }
        public int PosterUserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
