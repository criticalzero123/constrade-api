using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model
{
    public class PersonModel
    {
       
        public int Person_id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public int? AddressReference_id { get; set; }
        public DateTime? Birthdate { get; set; }

        public string? PhoneNumber { get; set; } = string.Empty;
    }
}
