using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("image_list")]
    public class ImageList
    {
        [Key]
        [Column("image_id")]
        public int ImageId { get; set; }

        [ForeignKey("Product")]
        [Column("product_id")]
        public int ProductId { get; set; }
        public Product product { get; set; }

        [Required]
        [Column("imageURL")]
        public string ImageURL { get; set; }
    }
}
