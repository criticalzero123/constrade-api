using ConstradeApi.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.VerificationEntity
{
    [Table("valid_identification")]
    public class ValidIdentification
    {
        [Key]
        public int ValidIdentificationId { get; set; }

        [Required]
        [Column("valid_id_number")]
        public string ValidIdNumber { get; set; } = string.Empty;

        [Required]
        [Column("valid_id_type")]
        public ValidIdType ValidIdType { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column("date_expiration")]
        public DateTime DateExpiration { get; set; }
    }
}
