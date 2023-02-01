using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Title { get; set; }

        [Required, StringLength(150)]
        [Column("description")]
        public string Description { get; set; }

        [StringLength(50)]
        [Column("model_number")]
        public string? ModelNumber { get; set; }

        [StringLength(50)]
        [Column("serial_number")]
        public string? SerialNumber { get; set; }

        [StringLength(100), Required]
        [Column("game_genre")]
        public string GameGenre{ get; set; }

        [StringLength(100), Required]
        [Column("platform")]
        public string Platform { get; set; }

        [Required]
        [Column("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [ Required]
        [Column("cash")]
        public decimal Cash { get; set; }

        [Required]
        [Column("item")]
        public string Item { get; set; }

        [ Required]
        [Column("date_created")]
        public DateTime DateCreated { get; set; }

        [ Required]
        [Column("count_favorite")]
        public int CountFavorite { get; set; }



        [Required, StringLength(20)]
        [Column("condition")]
        public string Condition { get; set; }

        [Required, StringLength(20)]
        [Column("prefere_trade")]
        public string PreferTrade { get; set; }

        [Required, StringLength(20)]
        [Column("delivery_method")]
        public string DeliveryMethod { get; set; }

        [Required]
        [Column("location")]
        public string Location { get; set; }

        [Required, StringLength(20)]
        [Column("product_status")]
        public string ProductStatus { get; set; }
    }
}
