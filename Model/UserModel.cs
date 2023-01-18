using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model
{
    public class UserModel
    {
        public int User_id { get; set; }
        
        public string User_type { get; set; } = string.Empty;

        public int PersonRefId { get; set; }

        public string Authprovider_type { get; set; } = string.Empty;

       
        public string Subscription_type { get; set; } = string.Empty;

       
        public string User_status { get; set; } = string.Empty;

        
        public string Email { get; set; }

       
        public string Password { get; set; }

     
        public string ImageUrl { get; set; }

    
        public DateTime LastActiveAt { get; set; }

       
        public int CountPost { get; set; }

        
        public DateTime DateCreated { get; set; }
    }
}
