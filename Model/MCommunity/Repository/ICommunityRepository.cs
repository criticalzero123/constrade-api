using ConstradeApi.Enums;

namespace ConstradeApi.Model.MCommunity.Repository
{
    public interface ICommunityRepository
    {
        Task<CommunityResponse> CreateCommunity(CommunityModel info);
        Task<IEnumerable<CommunityModel>> GetCommunityByOwnerId(int userId);
        Task<IEnumerable<CommunityModel>> GetCommunities();
        Task<CommunityModel?> GetCommunity(int id);
        Task<bool> DeleteCommunity(int id, int userId);
        Task<CommunityJoinResponse> JoinCommunity(int id, int userId);
    }
}
