using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user")]
    public class User
    {
        [Required, Key]
        public int User_id { get; set; }

        [ForeignKey("Person")]
        public int PersonRef_id { get; set; }
        public Person Person { get; set; }

        [Required]
        [StringLength(20)]
        public string User_type { get; set; } = string.Empty;

        [StringLength(20)]
        public string Authprovider_type { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Subscription_type { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string User_status { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime LastActiveAt { get; set; }

        [Required]
        public int CountPost { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
