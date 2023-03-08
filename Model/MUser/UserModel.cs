


using System.Security.Cryptography;

namespace ConstradeApi.Model.MUser
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string FirebaseId { get; set; }
        public string UserType { get; set; } = string.Empty;
        public int PersonRefId { get; set; }
        public string AuthProviderType { get; set; } = string.Empty;
        public string UserStatus { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime LastActiveAt { get; set; }
        public int CountPost { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
