namespace ConstradeApi.Model.MUser.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel?> Get(int id);
        Task<int>Save(UserInfoModel user);
        Task<bool> CheckEmailExist(string email);
        Task<bool> CheckPhoneExist(string phone);
        Task<IEnumerable<FavoriteModel>> GetFavorite(int userId);
        Task<bool> AddFavorite(int userId, int productId);
        Task<bool> DeleteFavorite(int id);
        Task<IEnumerable<UserFollowModel>> GetUserFollow(int userId);
        Task<IEnumerable<UserFollowModel>> GetUserFollower(int userId);
        Task<bool> FollowUser(int followUser, int followedByUser);
        Task<bool> AddReview(int reviewerId, UserReviewModel userReviewModel);
        Task<IEnumerable<UserReviewModel>> GetMyReviews(int userId);
        Task<IEnumerable<UserReviewModel>> GetReviews(int userId);
        Task<UserInfoModel?> LoginByGoogle(string email);
        Task<UserInfoModel?> LoginByEmailAndPassword(UserLoginInfoModel info);
    }
}
