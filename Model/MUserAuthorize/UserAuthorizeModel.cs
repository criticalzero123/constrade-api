
namespace ConstradeApi.Model.MUserAuthorize
{
    public class UserAuthorizeModel
    {
        public int ApiKeyId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
