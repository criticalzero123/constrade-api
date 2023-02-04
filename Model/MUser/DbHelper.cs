using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstradeApi.Model.MUser
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
        public List<UserModel> GetAll()
        {
            List<UserModel> response = new List<UserModel>();

            var data = _context.Users.ToList();
            data.ForEach(row => response.Add(new UserModel()
            {
                User_id = row.User_id,
                User_status = row.User_status,
                User_type = row.User_type,
                Authprovider_type = row.Authprovider_type,
                Subscription_type = row.Subscription_type,
                ImageUrl = row.ImageUrl,
                DateCreated = row.DateCreated,
                PersonRefId = row.PersonRef_id,
                Email = row.Email,
                LastActiveAt = row.LastActiveAt,
                CountPost = row.CountPost,
            }));

            return response;
        }

        /// <summary>
        /// Getting the information of specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null or an UserModel</returns>
        public UserModel? Get(int id)
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
                    User_id = o._user.User_id,
                    User_type = o._user.User_type,
                    PersonRefId = o._user.PersonRef_id,
                    Email = o._user.Email,
                    Authprovider_type = o._user.Authprovider_type,
                    Subscription_type = o._user.Subscription_type,
                    User_status = o._user.User_status,
                    Password = o._user.Password,
                    ImageUrl = o._user.ImageUrl,
                    LastActiveAt = o._user.LastActiveAt,
                    CountPost = o._user.CountPost,
                    DateCreated = o._user.DateCreated,
                    Person = new PersonModel()
                    {
                        Person_id = o._person.Person_id,
                        FirstName = o._person.FirstName,
                        LastName = o._person.LastName,
                        AddressReference_id = o._person.AddressRef_id,
                        Birthdate = o._person.Birthdate,
                        PhoneNumber = o._person.PhoneNumber,
                    }
                }).FirstOrDefault();


            return data;
        }

        /// <summary>
        /// POST for User
        /// </summary>
        /// <param name="user"></param>
        public void Save(UserModel user)
        {
            Person personTable = new Person();
            personTable.FirstName = user.Person.FirstName;
            personTable.LastName = user.Person.LastName;
            personTable.Birthdate = user.Person.Birthdate;
            _context.Persons.Add(personTable);

            _context.SaveChanges();

            User userTable = new User();
            userTable.User_type = user.User_type;
            userTable.PersonRef_id = personTable.Person_id;
            userTable.Authprovider_type = user.Authprovider_type;
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

        /// <summary>
        /// Check the email if it exist in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true or false</returns>
        public async Task<bool> CheckEmailExist(string email)
        {
            bool result = await _context.Users.AnyAsync(user => user.Email == email);

            return result;
        }

        /// <summary>
        /// Check the phone if it exist in the database
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>true or false</returns>
        public async Task<bool> CheckPhoneExist(string phone)
        {
            bool result = await _context.Persons.AnyAsync(person => person.PhoneNumber == phone);

            return result;
        }

        /// <summary>
        /// GET: the list of the favorites
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public  List<FavoriteModel> GetFavorite(int userId)
        {
            List<FavoriteModel> favoriteModels =  _context.UserFavorites.Where(_f => _f.UserId == userId).Select(_f => new FavoriteModel()
            {
                FavoriteId = _f.FavoriteId,
                UserId = _f.UserId,
                ProductId = _f.ProductId,
            }).ToList();

            return favoriteModels;
        }  

        /// <summary>
        /// POST for adding product to favorite
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddFavorite( int userId, int productId)
        {
            Product? product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.CountFavorite += 1;
            await _context.SaveChangesAsync();

            Favorites favorites= new Favorites();
            favorites.ProductId = productId;
            favorites.UserId = userId;
            favorites.Date = DateTime.Now;

            await _context.UserFavorites.AddAsync(favorites);
            await _context.SaveChangesAsync();

            return true;
        }
        
        /// <summary>
        /// DELETE for favorite of user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFavorite(int id)
        {
            Favorites? favorites = await _context.UserFavorites.FindAsync(id);
            if(favorites == null) return false;

            Product? product = await _context.Products.FindAsync(favorites.ProductId);
            if (product == null) return false;

            product.CountFavorite -= 1;
            await _context.SaveChangesAsync();

            _context.UserFavorites.Remove(favorites);
            await _context.SaveChangesAsync();


            return true;
        }

        /// <summary>
        /// Getting the User Follower
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Users ID</returns>
        public List<UserFollowModel> GetUserFollow(int userId)
        {
            List<UserFollowModel> userFollowModel = _context.UserFollows.Where(_u => _u.FollowedByUserId.Equals(userId)).Select(_u => new UserFollowModel()
            {
                FollowId = _u.FollowId,
                FollowedByUserId = _u.FollowedByUserId,
                DateFollowed = _u.DateFollowed
            }).ToList();

            return userFollowModel;
        }

        /// <summary>
        /// Getting the User Followed User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Users ID and date</returns>
        public List<UserFollowModel> GetUserFollower(int userId)
        {
            List<UserFollowModel> userFollowModels = _context.UserFollows.Where(_u => _u.FollowByUserId.Equals(userId)).Select(_u => new UserFollowModel()
            {
                FollowId = _u.FollowId,
                FollowByUserId= _u.FollowByUserId,
                DateFollowed = _u.DateFollowed
            }).ToList();

            return userFollowModels;
        }

        /// <summary>
        /// POST & DELETE: the followed user is the one who is the follower
        /// </summary>
        /// <param name="followUser"></param>
        /// <param name="followedUser"></param>
        public  bool FollowUser(int followUser, int followedByUser)
        {
            if(followUser == followedByUser) return false;

            List<Follow> follows =  _context.UserFollows.Where(_u => _u.FollowByUserId.Equals(followUser)).ToList();
            Follow? flag =  follows.Where(_u => _u.FollowedByUserId.Equals(followedByUser)).FirstOrDefault();

            //if the user already follows
            if(flag != null)
            {
                _context.UserFollows.Remove(flag);
            }
            else
            {
                Follow follow = new Follow()
                {
                    FollowByUserId = followUser,
                    FollowedByUserId = followedByUser,
                    DateFollowed = DateTime.Now,
                };

                _context.UserFollows.Add(follow);
               
            }
            _context.SaveChanges();
            return true;

        }
    }
}
