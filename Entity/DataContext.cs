using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Entity
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Address> Address { get; set; } 
        public DbSet<Person> Persons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ImageList> Images { get; set; }
        public DbSet<ProductComment > ProductComments { get; set; }
        public DbSet<Favorites> ProductFavorite { get; set; }
        public DbSet<Follow> UserFollows { get; set; }
        public DbSet<ProductView> ProductViews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Review> UserReviews { get; set; }
        public DbSet<Wallet> UserWallet { get; set; }
        public DbSet<TopUpTransaction> TopUpTransactions { get; set; }
        public DbSet<SendMoneyTransaction> SendMoneyTransactions { get; set; }  
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionHistory> SubscriptionsHistory { get; set; }  
        public DbSet<OneTimePassword> Otp { get; set; }
        public DbSet<ApiKey> ApiKey { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<UserMessage> UserMessage { get; set; }
        public DbSet<ProductChat> ProductChat { get; set; }
        public DbSet<ProductMessage> ProductMessages { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<SystemFeedback> SystemFeedback { get; set; }
        public DbSet<UserNotification> Notification { get; set; }
        public DbSet<Community> Community { get; set; }
        public DbSet<CommunityMember> CommunityMember { get; set; }
        public DbSet<CommunityJoin> CommunityJoin { get; set; }
        public DbSet<CommunityPost> CommunityPost { get; set; }
        public DbSet<CommunityPostComment> PostComment { get; set; }
        public DbSet<CommunityPostLike> PostLike { get; set; }
    }
}
