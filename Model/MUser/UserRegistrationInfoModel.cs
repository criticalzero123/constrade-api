﻿namespace ConstradeApi.Model.MUser
{
    public class UserRegistrationInfoModel
    {
        public string User_type { get; set; } = string.Empty;
        public string Authprovider_type { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int CountPost { get; set; } = 0;
        public string UserStatus { get; set; } = "active";
        public DateTime LastActiveAt { get; set; } = DateTime.Now;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime Birthdate { get; set; } = DateTime.Now;
    }
}
