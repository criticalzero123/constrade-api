using ConstradeApi.Entity;
using System.Linq;

namespace ConstradeApi.Model.MProduct
{
    public class DbHelper
    {
        private readonly DataContext _context;

        public DbHelper(DataContext context)
        {
            _context = context;
        }

        public List<ProductModel> GetProducts()
        {
            List<ProductModel> _products = new List<ProductModel>();

            List<Product> data = _context.Products.ToList<Product>();

            data.ForEach(row => _products.Add(new ProductModel()
            {
                ProductId = row.ProductId,
                Title = row.Title,
                Description = row.Description,
                Condition= row.Condition,
                PreferTrade= row.PreferTrade,
                DeliveryMethod= row.DeliveryMethod,
                ProductStatus= row.ProductStatus,
                Location= row.Location,
                ModelNumber= row.ModelNumber,
                SerialNumber= row.SerialNumber,
                GameGenre= row.GameGenre,
                Platform= row.Platform,
                ThumbnailUrl= row.ThumbnailUrl,
                Cash= row.Cash,
                Item= row.Item,
                DateCreated= row.DateCreated,
                CountFavorite= row.CountFavorite,
            })) ;
           
            return _products;
        }

        public void Save(ProductModel product)
        {
            Product _product = new Product()
            {
                PosterUserId= product.PosterUserId,
                Title = product.Title,
                Description = product.Description,
                Condition= product.Condition,
                PreferTrade= product.PreferTrade,
                DeliveryMethod= product.DeliveryMethod,
                ProductStatus= product.ProductStatus,
                Location= product.Location,
                ModelNumber= product.ModelNumber,
                SerialNumber= product.SerialNumber,
                GameGenre   = product.GameGenre,
                Platform= product.Platform,
                ThumbnailUrl= product.ThumbnailUrl,
                Cash= product.Cash,
                Item= product.Item,
                DateCreated= product.DateCreated,
                CountFavorite= product.CountFavorite,
            };

            _context.Products.Add( _product );
            _context.SaveChanges();
        }
    }
}
