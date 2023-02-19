using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("one_time_password")]
    [Index(nameof(SendTo))]
    public class OneTimePassword
    {
        [Key]
        public int OtpId { get; set; }
        
        [Column("send_to")]
        [Required]
        public string SendTo { get; set; } = string.Empty;

        [Column("otp")]
        public string OTP { get; set; } = string.Empty;

        [Column("expiration_time")]
        public DateTime ExpirationTime { get; set; } 
    }
}
