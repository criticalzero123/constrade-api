using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.VerificationEntity
{
    public class VerificationDataContext : DbContext
    {
        public VerificationDataContext(DbContextOptions<VerificationDataContext> option ) : base( option ) { }

        public DbSet<ValidIdentification> ValidIdentification { get; set; }
        public DbSet<ValidIdRequest> ValidIdRequests { get; set; }
        public DbSet<ProductPrices> ProductPrices { get; set; }
        public DbSet<AdminAccounts> AdminAccounts { get; set; }
    }
}
