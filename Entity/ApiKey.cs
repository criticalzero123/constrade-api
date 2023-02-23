using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("api_key")]
    [Index(nameof(Token))]
    public class ApiKey
    {
        [Key]
        [Column("api_key_id")]
        public int ApiKeyId { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("token")]
        public string Token { get; set; } = string.Empty;

        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } 
    }
}
