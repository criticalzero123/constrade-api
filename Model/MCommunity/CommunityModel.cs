using ConstradeApi.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MCommunity
{
    public class CommunityModel
    {
        public int CommunityId { get; set; }
        public int OwnerUserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Visibility { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int TotalMembers { get; set; }
    }
}
