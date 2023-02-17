namespace ConstradeApi.Model.MProduct
{
    public class ProductAndImages
    {
        public ProductModel Product { get; set; }
        public IEnumerable<string> ImageURLList { get; set; }
    }
}
