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
        /// <summary>
        /// GET: If the Current User follow the other user
        /// </summary>
        /// <param name="otherUserId"></param>
        /// <param name="currentUserId"></param>
        /// <returns>True if the user follows the other user otherwise False</returns>
        Task<bool> IsFollowUser(int otherUserId, int currentUserId);
        Task<bool> FollowUser(int followUser, int followedByUser);
        Task<bool> AddReview(int reviewerId, UserReviewModel userReviewModel);
        Task<IEnumerable<UserReviewModel>> GetMyReviews(int userId);
        Task<IEnumerable<UserReviewModel>> GetReviews(int userId);
        Task<UserAndPersonModel?> LoginByGoogle(string email);
        Task<UserAndPersonModel?> LoginByEmailAndPassword(UserLoginInfoModel info);
        Task<UserAndPersonModel?> UpdatePersonInfo(UserAndPersonModel info);
        Task<bool> ChangePasswordByEmail(ChangePasswordModel model);
    }
}
