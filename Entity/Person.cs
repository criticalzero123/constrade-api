using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("person")]
    public class Person
    {
        [Key,Required]
        public int Person_id { get; set; }

       
        [ForeignKey("Address")]
        public int AddressRef_id { get; set; }
        public  Address Address { get; set; } 

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime Birthdate { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
