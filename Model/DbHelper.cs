using ConstradeApi.Entity;
using System.Linq;

namespace ConstradeApi.Model
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns>List of Users</returns>
        public List<UserModel> GetUsers()
        {
            List<UserModel> response = new List<UserModel>();

            var data = _context.Users.ToList();
            data.ForEach(row => response.Add(new UserModel()
            {
                User_id = row.User_id,
                User_status= row.User_status,
                User_type= row.User_type, 
                Authprovider_type= row.Authprovider_type,
                Subscription_type= row.Subscription_type,
                ImageUrl= row.ImageUrl,
                DateCreated= row.DateCreated,
                PersonRefId = row.PersonRef_id,
                Email= row.Email,
                LastActiveAt= row.LastActiveAt,
                CountPost = row.CountPost,
            }));

            return response;
        }

        /// <summary>
        /// Getting the information of specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null or an UserModel</returns>
        public UserModel? GetUser(int id)
        {

            var userData = _context.Users.ToList();
            var personData = _context.Persons.ToList();

            var data = userData.Join(personData,
                                    _user => _user.PersonRef_id,
                                    _person => _person.Person_id,
                                    (_user, _person) => new
                                    {
                                        _user,
                                        _person
                                    })
                .Where(o => o._user.User_id == id)
                .Select(o => new UserModel()
                {
                    User_id= o._user.User_id,
                    User_type= o._user.User_type,
                    PersonRefId = o._user.PersonRef_id,
                    Email= o._user.Email,
                    Authprovider_type= o._user.Authprovider_type,
                    Subscription_type  = o._user.Subscription_type,
                    User_status= o._user.User_status,
                    Password= o._user.Password,
                    ImageUrl= o._user.ImageUrl,
                    LastActiveAt= o._user.LastActiveAt,
                    CountPost= o._user.CountPost,
                    DateCreated= o._user.DateCreated,
                    Person = new PersonModel()
                    {
                        Person_id = o._person.Person_id,
                        FirstName= o._person.FirstName,
                        LastName = o._person.LastName,
                        AddressReference_id = o._person.AddressRef_id,
                        Birthdate= o._person.Birthdate,
                        PhoneNumber= o._person.PhoneNumber,
                    }
                }).FirstOrDefault();


            return data;
        }
        /// <summary>
        /// POST or PUT for USER
        /// </summary>
        /// <param name="user"></param>
        public void SaveUser(UserModel user)
        {
            Person personTable = new Person();
            personTable.FirstName = user.Person.FirstName;
            personTable.LastName = user.Person.LastName;
            personTable.Birthdate = user.Person.Birthdate;
            _context.Persons.Add(personTable);

            _context.SaveChanges();

            User userTable = new User();
            userTable.User_type= user.User_type;
            userTable.PersonRef_id = personTable.Person_id;
            userTable.Authprovider_type= user.Authprovider_type;
            userTable.Subscription_type = user.Subscription_type;
            userTable.User_status = user.User_status;
            userTable.Email = user.Email;
            userTable.Password = user.Password;
            userTable.ImageUrl = user.ImageUrl;
            userTable.CountPost = 0;
            userTable.DateCreated = DateTime.Now;
            userTable.LastActiveAt = DateTime.Now;
            _context.Users.Add(userTable);

            _context.SaveChanges();
        }
    }
}
