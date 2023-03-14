using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUser
{
    public class UserAndPersonModel
    {
        public UserModel User{ get; set; }
        public PersonModel Person { get; set; } 
        public decimal? Rate { get; set; }
    }
}
