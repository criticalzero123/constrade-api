using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.Model.MUser
{
    public class UserFollowModel
    {
     
        public int FollowId { get; set; }

        
        public int FollowByUserId { get; set; }

     
        public int FollowedByUserId { get; set; }

      
        public DateTime DateFollowed { get; set; }
    }
}
