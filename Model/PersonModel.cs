using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model
{
    public class PersonModel
    {
       
        public int Person_id { get; set; }
      
        public string Name { get; set; } = string.Empty;


        public DateTime Birthdate { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
