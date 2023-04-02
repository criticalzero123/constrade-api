using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MProduct
{
    public class ProductFullDetails
    {
        public ProductModel Product { get; set; }
        public IEnumerable<ImageListModel> Images { get; set; }
        public UserModel User { get; set; }
        public PersonModel Person { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsBoosted { get; set; }
    }
}
