
namespace ConstradeApi.Model.MUser
{
    public class FavoriteModel
    {
    
        public int FavoriteId { get; set; }

    
        public int ProductId { get; set; }
        
        public int UserId { get; set; }
    
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
