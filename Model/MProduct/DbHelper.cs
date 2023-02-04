using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        ///  GET All
        /// </summary>
        /// <returns>List of Products</returns>
        public List<ProductModel> GetAllProducts()
        {
            List<ProductModel> _products = new List<ProductModel>();

            List<Product> data = _context.Products.ToList();

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

        /// <summary>
        /// POST for product
        /// </summary>
        /// <param name="product"></param>
        public void Save(ProductModel product, List<string> imageList)
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

            // Adding the images in image list table
            foreach ( string item in imageList ) 
            {
                _context.Images.Add(new ImageList()
                {
                    ProductId = _product.ProductId,
                    ImageURL = item
                });
            }

            _context.SaveChanges();
        }

        public async Task<ProductModel?> Get(int id)
        {
            var _data = await _context.Products.Where(p => p.ProductId == id).Select(product => new ProductModel()
            {
                PosterUserId = product.PosterUserId,
                Title = product.Title,
                Description = product.Description,
                Condition = product.Condition,
                PreferTrade = product.PreferTrade,
                DeliveryMethod = product.DeliveryMethod,
                ProductStatus = product.ProductStatus,
                Location = product.Location,
                ModelNumber = product.ModelNumber,
                SerialNumber = product.SerialNumber,
                GameGenre = product.GameGenre,
                Platform = product.Platform,
                ThumbnailUrl = product.ThumbnailUrl,
                Cash = product.Cash,
                Item = product.Item,
                DateCreated = product.DateCreated,
                CountFavorite = product.CountFavorite,
            }).FirstOrDefaultAsync();

            return _data;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            Product? product = await _context.Products.FindAsync(id);

            if(product == null) return false;
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProduct(int id, ProductModel product)
        {
            Product? _product = await _context.Products.FindAsync(id);
            if(_product == null) return false;

            _product.Title = product.Title;
            _product.Description= product.Description;
            _product.ModelNumber = product.ModelNumber;
            _product.SerialNumber = product.SerialNumber;
            _product.GameGenre = product.GameGenre;
            _product.Platform = product.Platform;
            _product.ThumbnailUrl = product.ThumbnailUrl;
            _product.Cash = product.Cash;
            _product.Item = product.Item;
            _product.Location = product.Location;
            _product.Condition = product.Condition;
            _product.PreferTrade= product.PreferTrade;
            _product.ThumbnailUrl = product.ThumbnailUrl;
            _product.DeliveryMethod = product.DeliveryMethod;


            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddCommentProduct(int productId, int userId, string comment)
        {
            Product? _product = await _context.Products.FindAsync(productId);


        }
    }
}
