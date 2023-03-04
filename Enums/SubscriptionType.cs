namespace ConstradeApi.Enums
{
    public enum SubscriptionType
    {
        Free,
        Premium
    }

    public enum SubscriptionResponseType
    {
        AlreadyPremium,
        NotFullyVerified,
        NotEnoughBalance,
        UserNotFound,
        Success,
    }
}
