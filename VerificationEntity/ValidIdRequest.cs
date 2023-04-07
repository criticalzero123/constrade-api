using ConstradeApi.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.VerificationEntity
{
    [Table("valid_id_request")]
    [Index("UserId")]
    public class ValidIdRequest
    {
        [Key]
        [Column("valid_id_request_id")]
        public int ValidIdRequestId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("username")]
        public string UserName { get; set; } = string.Empty;

        [Column("validation_type")]
        public ValidIdType ValidationType { get; set; }

        [Column("valid_id_number")]
        public string ValidIdNumber { get; set; } = string.Empty;

        [Column("request_date")]
        public DateTime RequestDate { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;
    }
}
