using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Services.EntityToModel;
using ConstradeApi.Services.Password;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstradeApi.Model.MUser.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <returns>List of Users</returns>
        public async Task<IEnumerable<UserModel>> GetAll()
        {
            List<UserModel> response = new List<UserModel>();

            var data = await _context.Users.ToListAsync();
            data.ForEach(row => response.Add(row.ToModel()));

            return response;
        }

        /// <summary>
        /// Getting the information of specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>null or an UserModel</returns>
        public async Task<UserAndPersonModel?> Get(int id)
        {
            var userData =  await _context.Users.ToListAsync();
            var personData = await  _context.Persons.ToListAsync();

            var data = userData.Join(personData,
                                    _user => _user.PersonRefId,
                                    _person => _person.Person_id,
                                    (_user, _person) => new
                                    {
                                        _user,
                                        _person
                                    })
                .Where(o => o._user.UserId == id)
                .Select(o => new UserAndPersonModel()
                {
                    User = o._user.ToModel(),
                    Person = o._person.ToModel(),
                }).FirstOrDefault();


            return data;
        }

        /// <summary>
        /// POST: creating user
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Primary Key of the User</returns>
        public async Task<int> Save(UserAndPersonModel info)
        {
            Person personTable = new Person();
            personTable.FirstName = info.Person.FirstName;
            personTable.LastName = info.Person.LastName;
            personTable.Birthdate = null;
            await _context.Persons.AddAsync(personTable);

            await _context.SaveChangesAsync();

            User userTable = new User();
            userTable.FirebaseId = info.User.FirebaseId;
            userTable.UserType = info.User.UserType;
            userTable.PersonRefId = personTable.Person_id;
            userTable.AuthProviderType = info.User.AuthProviderType;
            userTable.UserStatus = info.User.UserStatus;
            userTable.Email = info.User.Email.ToLower();
            userTable.Password = info.User.Password != null ? PasswordHelper.Hash(info.User.Password) : "";
            userTable.ImageUrl = info.User.ImageUrl;
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
            var result = await _context.Users.Where(user => user.Email == email && user.UserStatus == "active").FirstOrDefaultAsync();

            return result != null;
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
        public async Task<IEnumerable<FavoriteModel>> GetFavorite(int userId)
        {
            List<FavoriteModel> favoriteModels = await _context.ProductFavorite
                .Where(_f => _f.UserId == userId)
                .Select(_f => _f.ToModel())
                .ToListAsync();

            return favoriteModels;
        }

        /// <summary>
        /// POST for adding product to favorite
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> AddFavorite(int userId, int productId)
        {
            Product? product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            product.CountFavorite += 1;
            await _context.SaveChangesAsync();

            Favorites favorites = new Favorites();
            favorites.ProductId = productId;
            favorites.UserId = userId;
            favorites.Date = DateTime.Now;

            await _context.ProductFavorite.AddAsync(favorites);
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
            Favorites? favorites = await _context.ProductFavorite.FindAsync(id);
            if (favorites == null) return false;

            Product? product = await _context.Products.FindAsync(favorites.ProductId);
            if (product == null) return false;

            product.CountFavorite -= 1;
            await _context.SaveChangesAsync();

            _context.ProductFavorite.Remove(favorites);
            await _context.SaveChangesAsync();


            return true;
        }

        /// <summary>
        /// Getting the User Follower
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Users ID</returns>
        public async Task<IEnumerable<UserFollowModel>> GetUserFollow(int userId)
        {
            List<UserFollowModel> userFollowModel = await _context.UserFollows
                .Where(_u => _u.FollowedByUserId.Equals(userId))
                .Select(_u => _u.ToModel()).ToListAsync();

            return userFollowModel;
        }

        /// <summary>
        /// Getting the User Followed User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Users ID and date</returns>
        public async Task<IEnumerable<UserFollowModel>> GetUserFollower(int userId)
        {
            List<UserFollowModel> userFollowModels = await _context.UserFollows
                .Where(_u => _u.FollowByUserId.Equals(userId))
                .Select(_u => _u.ToModel()).ToListAsync();

            return userFollowModels;
        }

        /// <summary>
        /// POST & DELETE: the followed user is the one who is the follower
        /// </summary>
        /// <param name="followUser"></param>
        /// <param name="followedUser"></param>
        public async Task<bool> FollowUser(int followUser, int followedByUser)
        {
            if (followUser == followedByUser) return false;

            //Get the users followers
            List<Follow> follows = await _context.UserFollows.Where(_u => _u.FollowByUserId.Equals(followUser)).ToListAsync();
            //Check if the current user follows him
            Follow? flag =  follows.FirstOrDefault(_u => _u.FollowedByUserId.Equals(followedByUser));


            //if the user already follows
            if (flag != null)
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

                await _context.UserFollows.AddAsync(follow);

            }
            await _context.SaveChangesAsync();
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
                Description = userReviewModel.Description,
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
        public async Task<IEnumerable<ReviewDisplayModel>> GetMyReviews(int userId, int otherUserId)
        {
            List<Transaction> _transaction = await _context.Transactions.Include(_t => _t.Buyer.Person)
                                                                        .Where(_t => _t.BuyerUserId == userId && _t.SellerUserId == otherUserId && _t.IsReviewed)
                                                                        .ToListAsync();
            List<Review> _reviews = _context.UserReviews.ToList();


            IEnumerable<ReviewDisplayModel> data = _reviews.Join(_transaction,
                                    _r => _r.TransactionRefId,
                                    _t => _t.TransactionId,
                                    (_r, _t) => new { _r, _t }
                                  )
                  .OrderByDescending(result => result._r.DateCreated)
                  .Select(result => new ReviewDisplayModel
                  {
                      ReviewId = result._r.ReviewId,
                      Rate = result._r.Rate,
                      TransactionId = result._t.TransactionId,
                      UserName = result._t.Buyer.Person.FirstName + " " + result._t.Buyer.Person.LastName,
                      ImageUrl = result._t.Buyer.ImageUrl,
                      Description = result._r.Description,
                      Date = result._r.DateCreated,
                      ProductId = result._t.ProductId,
                  });

            return data;
        }

        /// <summary>
        /// GET: transaction review about the seller
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ReviewDisplayModel>> GetReviews(int userId, int otherUserId)
        {
            List<Transaction> _transaction = await _context.Transactions.Include(_t => _t.Buyer.Person)
                                                                        .Where(_t => _t.SellerUserId == otherUserId && _t.BuyerUserId != userId && _t.IsReviewed)
                                                                        .ToListAsync();
            List<Review> _reviews = _context.UserReviews.ToList();


            IEnumerable<ReviewDisplayModel> data = _reviews.Join(_transaction,
                                      _r => _r.TransactionRefId,
                                      _t => _t.TransactionId,
                                      (_r, _t) => new { _r, _t }
                                    )
                .OrderByDescending(result => result._r.DateCreated)
                .Select(result => new ReviewDisplayModel
                {
                    ReviewId = result._r.ReviewId,
                    TransactionId = result._t.TransactionId,
                    Rate = result._r.Rate,
                    UserName = result._t.Buyer.Person.FirstName + " " + result._t.Buyer.Person.LastName,
                    ImageUrl = result._t.Buyer.ImageUrl,
                    Description = result._r.Description,
                    Date = result._r.DateCreated,
                    ProductId = result._t.ProductId,
                });

            return data;
        }

        public async Task<decimal> GetAverage(int userId)
        {
            IEnumerable<Transaction> transactions = await _context.Transactions.Where(_t => _t.SellerUserId == userId && _t.IsReviewed).ToListAsync();

            if (transactions.Count() == 0) return 0;

            IEnumerable<decimal> rates = _context.UserReviews.ToList().Join(transactions,
                                                               _r => _r.TransactionRefId,
                                                               _t => _t.TransactionId,
                                                               (_r, _t) => new { _r, _t }
                                                               )
                                                            .Select(result => Convert.ToDecimal(result._r.Rate)).ToList();

            return rates.Average();
        }

        /// <summary>
        /// PUT: Getting the user info by using google auth
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserModel or NULL</returns>
        /// 
        //TODO: please make a checker if the user status is active or not
        public async Task<UserAndPersonModel?> LoginByGoogle(string email)
        {
            User? user = _context.Users.Where(_u => _u.Email.Equals(email) && _u.UserStatus == "active").FirstOrDefault();
            if (user == null) return null;

            if (!user.AuthProviderType.Equals("google")) return null;

            user.LastActiveAt = DateTime.Now;
            await _context.SaveChangesAsync();

            Person data = _context.Persons.Find(user.PersonRefId)!;

            return new UserAndPersonModel()
            {
                User = user.ToModel(),
                Person = data.ToModel()
            };
        }

        /// <summary>
        /// PUT: Gettint the user info by using email and password
        /// </summary>
        /// <param name="info"></param>
        /// <returns>UserInfoModel or NULL</returns>
        public async Task<UserAndPersonModel?> LoginByEmailAndPassword(UserLoginInfoModel info)
        {
            User? user = _context.Users.Where(_u => _u.Email.Equals(info.Email) && _u.Password.Equals(PasswordHelper.Hash(info.Password!))).FirstOrDefault();
            if (user == null || !user.AuthProviderType.Equals("email") || user.UserStatus != "active") return null;

            user.LastActiveAt = DateTime.Now;
            await _context.SaveChangesAsync();

            Person data = _context.Persons.Find(user.PersonRefId)!;

            return new UserAndPersonModel()
            {
                User = user.ToModel(),
                Person = data.ToModel()
            };
        }
        public async Task<UserAndPersonModel?> UpdatePersonInfo(UserAndPersonModel info)
        {
            Person? person = await _context.Persons.FindAsync(info.Person.PersonId);

            if (person == null) return null;

            User user = await _context.Users.Where(_u => _u.PersonRefId.Equals(info.Person.PersonId)).FirstAsync();

            user.ImageUrl = info.User.ImageUrl;
            user.BackgroundImageUrl = info.User.BackgroundImageUrl;

            person.FirstName= info.Person.FirstName;
            person.LastName= info.Person.LastName; 
            person.Birthdate = info.Person.Birthdate;

            person.Gender = info.Person.Gender;
            await _context.SaveChangesAsync();

            return new UserAndPersonModel()
            {
                User = user.ToModel(),
                Person = person.ToModel()
            }; 
        }
        public async Task<bool> IsFollowUser(int otherUserId, int currentUserId)
        {
            Follow? flag = await _context.UserFollows.Where(_f => _f.FollowByUserId == otherUserId && _f.FollowedByUserId == currentUserId).FirstOrDefaultAsync();

            if (flag == null) return false;

            return true;
        }
        public async Task<bool> ChangePasswordByEmail(ChangePasswordModel model)
        {
            User? user = await _context.Users.Where(_u => _u.Email.Equals(model.Email)).FirstOrDefaultAsync();

            if(user == null || !user.AuthProviderType.Equals("email")) return false;

            user.Password = PasswordHelper.Hash(model.NewPassword);
            await _context.SaveChangesAsync();

            return true; 
        }

        public async Task<IEnumerable<TransactionModel>> GetNotRated( int buyerId, int sellerId)
        {
            IEnumerable<TransactionModel> _transactions = await _context.Transactions.Where(_u =>  !_u.IsReviewed && _u.BuyerUserId == buyerId && _u.SellerUserId == sellerId)
                                                                                     .Select(_t => _t.ToModel())
                                                                                     .ToListAsync();

            return _transactions;
        }

        public async Task<UserFollowCount> GetUserFollowCount(int userId)
        {
            int userFollowCount = await _context.UserFollows.Where(_u => _u.FollowByUserId.Equals(userId))
                                                            .CountAsync();

            int userFollowedCount = await _context.UserFollows.Where(_u => _u.FollowedByUserId.Equals(userId))
                                                            .CountAsync();

            return new UserFollowCount
            {
                FollowCount = userFollowCount,
                FollowedCount = userFollowedCount
            };
        }

        public async Task<WalletResponseType> AddCountPost(int userId, int counts)
        {
            Wallet wallet = await _context.UserWallet.Where(_w => _w.UserId == userId).FirstAsync();
            int priceEachCount = 1;
            int totalPrice = counts * priceEachCount;

            if(wallet.Balance < totalPrice) return WalletResponseType.NotEnough;

            User user = await _context.Users.Where(u => u.UserId == userId).FirstAsync();
            wallet.Balance -= totalPrice;
            user.CountPost += counts;
            await _context.SaveChangesAsync();  

            return WalletResponseType.Success;
        }
    }
}
