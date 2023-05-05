using ConstradeApi.Enums;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MTransaction;

namespace ConstradeApi.Model.MUser.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserAndPersonModel?> Get(int id);
        Task<int>Save(UserAndPersonModel user);
        Task<bool> CheckEmailExist(string email);
        Task<bool> CheckPhoneExist(string phone);
        Task<IEnumerable<FavoriteModel>> GetFavorite(int userId);
        Task<bool> AddFavorite(int userId, int productId);
        Task<bool> DeleteFavorite(int id);
        Task<IEnumerable<UserFollowModel>> GetUserFollow(int userId);
        Task<IEnumerable<UserFollowModel>> GetUserFollower(int userId);
        Task<IEnumerable<UserAndPersonModel>> GetUserFollowerUsers(int userId);
        Task<IEnumerable<UserAndPersonModel>> GetUserFollowUsers(int userId);

        Task<UserFollowCount> GetUserFollowCount(int userId);
        /// <summary>
        /// GET: If the Current User follow the other user
        /// </summary>
        /// <param name="otherUserId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>True if the user follows the other user otherwise False</returns>
        Task<bool> IsFollowUser(int otherUserId, int currentUserId);
        Task<bool> FollowUser(int followUser, int followedByUser);
        Task<bool> AddReview(int reviewerId, UserReviewModel userReviewModel);
        Task<IEnumerable<ReviewDisplayModel>> GetMyReviews(int userId, int otherUserId);
        Task<IEnumerable<ReviewDisplayModel>> GetReviews(int userId, int otherUserId);
        Task<IEnumerable<ReviewDisplayModel>> GetMyReviewsMade(int userId);
        Task<UserAndPersonModel?> LoginByGoogle(string email);
        Task<UserAndPersonModel?> LoginByEmailAndPassword(UserLoginInfoModel info);
        Task<UserAndPersonModel?> UpdatePersonInfo(UserAndPersonModel info);
        Task<bool> ChangePasswordByEmail(ChangePasswordModel model);
        Task<IEnumerable<TransactionModel>> GetNotRated(int buyerId, int sellerId);
        Task<decimal> GetAverage(int userId);
        Task<WalletResponseType> AddCountPost(int userId, int counts);
        Task<string> GetUserTypeById(int userId);
        Task<int> GetUnreadNotif(int userId);
        Task<bool> MarkAsReadNotif(int id);
      
    }
}
