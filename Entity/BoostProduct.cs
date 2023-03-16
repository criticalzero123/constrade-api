using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("boost_product")]
    public class BoostProduct
    {
        [Key]
        [Column("boost_product_id")]
        public int BoostProductId { get; set; }
        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Column("days_boosted")]
        public int DaysBoosted { get; set; }
        [Column("status")]
        public string Status { get; set; } = string.Empty;
        [Column("date_time_expired")]
        public DateTime DateTimeExpired { get; set; }
        [Column("date_boosted")]
        public DateTime DateBoosted { get; set; }


    }
}
