
namespace ConstradeApi.Model.MUserApiKey
{
    public class ApiKeyModel
    {
        public int ApiKeyId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public bool IsActive { get; set; }
    }
}
