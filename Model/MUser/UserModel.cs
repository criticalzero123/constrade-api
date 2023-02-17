


namespace ConstradeApi.Model.MUser
{
    public class UserModel
    {
        public int User_id { get; set; }

        public string FirebaseId { get; set; }

        public string User_type { get; set; } = string.Empty;

        public int PersonRefId { get; set; }
        public PersonModel Person { get; set; }

        public string Authprovider_type { get; set; } = string.Empty;

        public string User_status { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;


        public string ImageUrl { get; set; } = string.Empty;


        public DateTime LastActiveAt { get; set; }


        public int CountPost { get; set; }


        public DateTime DateCreated { get; set; }
    }
}
