using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.VerificationEntity
{
    [Table("product_prices")]
    [Index("Name")]
    public class ProductPrices
    {
        [Key]
        public int ProductPricesId { get; set; }

        [ForeignKey("AdminAccount")]
        public int AddedBy { get; set; }
        public AdminAccounts AdminAccount { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Value { get; set; }
        [Column("release_date")]
        public DateTime? ReleaseDate { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        [Column("origin_url")]
        public string OriginUrl { get; set; } = string.Empty;

        [Column("shop_name")]
        public string ShopName { get; set; } = string.Empty;
    }
}
