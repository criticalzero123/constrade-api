using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MUser
{
    public class DbHelperUser
    {
        private readonly DataContext _context;

        public DbHelperUser(DataContext context)
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
                User_id = row.UserId,
                User_status = row.UserStatus,
                User_type = row.UserType,
                Authprovider_type = row.AuthProviderType,
                ImageUrl = row.ImageUrl,
                DateCreated = row.DateCreated,
                PersonRefId = row.PersonRefId,
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
                                    _user => _user.PersonRefId,
                                    _person => _person.Person_id,
                                    (_user, _person) => new
                                    {
                                        _user,
                                        _person
                                    })
                .Where(o => o._user.UserId == id)
                .Select(o => new UserModel()
                {
                    User_id = o._user.UserId,
                    User_type = o._user.UserType,
                    PersonRefId = o._user.PersonRefId,
                    Email = o._user.Email,
                    Authprovider_type = o._user.AuthProviderType,
                    User_status = o._user.UserStatus,
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
        /// POST: creating user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Primary Key of the User</returns>
        public async Task<int> Save(UserInfoModel user)
        {
            Person personTable = new Person();
            personTable.FirstName = user.FirstName;
            personTable.LastName = user.LastName;
            personTable.Birthdate = user.Birthdate;
            await _context.Persons.AddAsync(personTable);

            await _context.SaveChangesAsync();

            User userTable = new User();
            userTable.UserType = user.User_type;
            userTable.PersonRefId = personTable.Person_id;
            userTable.AuthProviderType = user.Authprovider_type;
            userTable.UserStatus = user.UserStatus;
            userTable.Email = user.Email.ToLower();
            userTable.Password = user.Password;
            userTable.ImageUrl = user.ImageUrl;
            userTable.CountPost = 0;
            userTable.DateCreated = DateTime.Now;
            userTable.LastActiveAt = DateTime.Now;
            await _context.Users.AddAsync(userTable);

            await _context.SaveChangesAsync();

            return userTable.UserId;
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
                FollowByUserId = _u.FollowByUserId,
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
                FollowedByUserId = _u.FollowedByUserId,
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

        /// <summary>
        /// Adding Review to a user that has a transaction record
        /// </summary>
        /// <param name="reviewerId"></param>
        /// <param name="userReviewModel"></param>
        /// <returns>false if the transaction is not found or already has review to the seller</returns>
        public async Task<bool> AddReview(int reviewerId, UserReviewModel userReviewModel)
        {
            Transaction? transaction = await _context.Transactions.FindAsync(userReviewModel.TransactionRefId);

            //Checking if the transcation already has a review or the transaction not existed
            if (transaction == null || transaction.IsReviewed) return false;

            if (!reviewerId.Equals(transaction.BuyerUserId)) return false;

            await _context.UserReviews.AddAsync(new Review()
            {
                TransactionRefId = userReviewModel.TransactionRefId,
                Rate = userReviewModel.Rate,
                DateCreated = userReviewModel.DateCreated    
            });
            await _context.SaveChangesAsync();


            transaction.IsReviewed = true;
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// GET: transaction reviews of the buyer or the user created reviews 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserReviewModel>> GetMyReviews(int userId)
        {
            List<Transaction> _transaction = await _context.Transactions.Where(_t => _t.BuyerUserId.Equals(userId)).Where(_t => _t.IsReviewed == true).ToListAsync();
            List<Review> _reviews = _context.UserReviews.ToList();


            var data = _reviews.Join(_transaction,
                                      _r => _r.TransactionRefId,
                                      _t => _t.TransactionId,
                                      (_r, _t) => new {_r,_t}
                                    )
                .Select(result => new UserReviewModel()
                {
                    ReviewId = result._r.ReviewId,
                    TransactionRefId = result._t.TransactionId,
                    Rate = result._r.Rate,
                    DateCreated = result._r.DateCreated
                }).ToList();

            return data;
        }

        /// <summary>
        /// GET: transaction review about the seller
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<UserReviewModel>> GetReviews(int userId)
        {
            List<Transaction> _transaction = await _context.Transactions.Where(_t => _t.SellerUserId.Equals(userId)).Where(_t => _t.IsReviewed == true).ToListAsync();
            List<Review> _reviews = _context.UserReviews.ToList();


            var data = _reviews.Join(_transaction,
                                      _r => _r.TransactionRefId,
                                      _t => _t.TransactionId,
                                      (_r, _t) => new { _r, _t }
                                    )
                .Select(result => new UserReviewModel()
                {
                    ReviewId = result._r.ReviewId,
                    TransactionRefId = result._t.TransactionId,
                    Rate = result._r.Rate,
                    DateCreated = result._r.DateCreated
                }).ToList();

            return data;
        }

        /// <summary>
        /// PUT: Getting the user info by using google auth
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserModel or NULL</returns>
        /// 
        //TODO: please make a checker if the user status is active or not
        public async Task<UserInfoModel?> LoginByGoogle(string email)
        {
            User? user = _context.Users.Where(_u => _u.Email.Equals(email)).FirstOrDefault();
            if (user == null) return null;

            user.LastActiveAt = DateTime.Now;
            await _context.SaveChangesAsync();

            Person data = _context.Persons.Find(user.PersonRefId)!;

            return new UserInfoModel()
            {
                UserId = user.UserId,
                PersonId = data.Person_id,
                User_type = user.UserType,
                Authprovider_type = user.AuthProviderType,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = user.Email,
                Password = user.Password,
                ImageUrl = user.ImageUrl,
                CountPost = user.CountPost,
                UserStatus = user.UserStatus,
                LastActiveAt = user.LastActiveAt,
                DateCreated = user.DateCreated,
                Birthdate = data.Birthdate,
            };
        }

        /// <summary>
        /// PUT: Gettint the user info by using email and password
        /// </summary>
        /// <param name="info"></param>
        /// <returns>UserInfoModel or NULL</returns>
        public async Task<UserInfoModel?> LoginByEmailAndPassword(UserLoginInfoModel info)
        {
            User? user  =  _context.Users.Where(_u => _u.Email.Equals(info.Email)).Where(_u => _u.Password.Equals(info.Password)).FirstOrDefault();
            if (user == null) return null;

            user.LastActiveAt= DateTime.Now;
            await _context.SaveChangesAsync();

            Person data = _context.Persons.Find(user.PersonRefId)!;

            return new UserInfoModel()
            {
                UserId = user.UserId,
                PersonId = data.Person_id,
                User_type = user.UserType,
                Authprovider_type = user.AuthProviderType,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = user.Email,
                Password = user.Password,
                ImageUrl = user.ImageUrl,
                CountPost = user.CountPost,
                UserStatus = user.UserStatus,
                LastActiveAt = user.LastActiveAt,
                DateCreated = user.DateCreated,
                Birthdate = data.Birthdate,
            };
        }
    }
}
