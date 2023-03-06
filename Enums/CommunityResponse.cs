namespace ConstradeApi.Enums
{
    public enum CommunityResponse
    {
        Success,
        NotVerified,
        Failed,
    }

    public enum CommunityJoinResponse
    {
        Pending,
        Approved,
        Rejected,
        Failed
    }

    public enum CommunityPostResponse
    {
        Success,
        NotAMember,
        Failed,
    }
}
