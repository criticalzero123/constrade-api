using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("address")]
    public class Address
    {
        [Required, Key]
        public int Address_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Barangay { get; set; } = string.Empty;

        [StringLength(50)]
        public string City { get; set; } = string.Empty;

        [StringLength(50)]
        public string Province { get; set; } = string.Empty;

        [StringLength(50)]
        public string Postal_code { get; set; } = string.Empty;

        [StringLength(50)]
        public string House_number { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Updated_at { get; set; } = string.Empty;
    }
}
