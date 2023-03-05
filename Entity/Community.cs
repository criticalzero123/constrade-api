using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstradeApi.Entity
{
    [Table("community")]
    public class Community
    {
        [Key]
        [Column("community_id")]
        public int CommunityId { get; set; }

        [ForeignKey("User")]
        [Column("owner_user_id")]
        public int OwnerUserId { get; set; }
        public User User { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("image_url")]
        public string ImageUrl { get; set; } = string.Empty; 

        [Column("visibility")]
        public string Visibility { get; set; } = string.Empty;

        [Column("date_created")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column("total_members")]
        public int TotalMembers { get; set; }

    }
}
