using ConstradeApi.Model.MUser;

namespace ConstradeApi.Model.MProduct
{
    public class ProductUserModel
    {
        public ProductModel Product { get; set; }
        public UserAndPersonModel UserInfo { get; set; }
    }
}
