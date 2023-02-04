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
        public DbSet<Favorites> UserFavorites { get; set; }
        public DbSet<Follow> UserFollows { get; set; }
    }
}
