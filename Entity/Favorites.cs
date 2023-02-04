using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("favorites")]
    public class Favorites
    {
        [Key]
        [Column("favorite_id")]
        public int FavoriteId { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("date")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
