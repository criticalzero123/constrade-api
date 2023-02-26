using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace ConstradeApi.Entity
{
    [Table("product")]
    public class Product
    {
        [Key, Required]
        [Column("product_id")]
        public int ProductId { get; set; }

        [ForeignKey("User")]
        [Column("poster_user_id")]
        public int PosterUserId { get; set; }
        public User User { get; set; }

        [Required, StringLength(50)]

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required, StringLength(150)]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("model_number")]
        public string? ModelNumber { get; set; }

        [StringLength(50)]
        [Column("serial_number")]
        public string? SerialNumber { get; set; }

        [StringLength(100), Required]
        [Column("game_genre")]
        public string GameGenre { get; set; } = string.Empty;

        [StringLength(100), Required]
        [Column("platform")]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [Column("thumbnail_url")]
        public string ThumbnailUrl { get; set; } = string.Empty;

        [Required]
        [Column("cash")]
        public decimal Cash { get; set; }

        [Required]
        [Column("item")]
        public string Item { get; set; } = string.Empty;

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [Column("count_favorite")]
        public int CountFavorite { get; set; } = 0;



        [Required, StringLength(20)]
        [Column("condition")]
        public string Condition { get; set; } = string.Empty;

        [Required, StringLength(20)]
        [Column("prefer_trade")]
        public string PreferTrade { get; set; } = string.Empty;

        [Required, StringLength(20)]
        [Column("delivery_method")]
        public string DeliveryMethod { get; set; } = string.Empty;

        [Required]
        [Column("location")]
        public string Location { get; set; } = string.Empty;

        [StringLength(20)]
        [Column("product_status")]
        public string ProductStatus { get; set; } = "unsold";

        [Required, Column("has_receipts")]
        public bool HasReceipts { get; set; } = false;
        
        [Required, Column("has_warranty")]
        public bool HasWarranty { get; set; } = false;

    }
}
