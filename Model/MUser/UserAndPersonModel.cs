using ConstradeApi.Entity;

namespace ConstradeApi.Model.MUser
{
    public class UserAndPersonModel
    {
        public UserAndPersonModel(UserModel user, PersonModel person)
        {
            User = user;
            Person = person;
        }

        public UserAndPersonModel(UserModel user, PersonModel person, decimal? rate)
        {
            User = user;
            Person = person;
            Rate = rate;
        }

        public UserAndPersonModel()
        {

        }

        public UserModel User{ get; set; }
        public PersonModel Person { get; set; } 
        public decimal? Rate { get; set; }
    }
}
