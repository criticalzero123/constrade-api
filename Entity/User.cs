using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("user")]
    public class User
    {
        [Required, Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("Person")]
        [Column("person_ref_id")]
        public int PersonRefId { get; set; }
        public Person Person { get; set; }

        [Required]
        [StringLength(20)]
        [Column("user_type")]
        public string UserType { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("auth_provider_type")]
        public string AuthProviderType { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("subscription_type")]
        public string SubscriptionType { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        [Column("user_status")]
        public string UserStatus { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [StringLength(255)]
        [Column("password")]

        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [Column("image_url")]

        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Column("last_active_at")]
        public DateTime LastActiveAt { get; set; } = DateTime.Now;

        [Required]
        [Column("count_post")]

        public int CountPost { get; set; }


        [Required]
        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
