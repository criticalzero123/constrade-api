using ConstradeApi.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstradeApi.Model.MProduct
{
    public class DbHelperProduct
    {
        private readonly DataContext _context;

        public DbHelperProduct(DataContext context)
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
        public async Task Save(ProductModel product, IEnumerable<string> imageList)
        {
            User? user = await _context.Users.FindAsync(product.PosterUserId);
            if (user == null) return;
            //Prevent the user that less than 1 count post to post a product
            if (user.CountPost < 1) return;

            user.CountPost -= 1;
            await _context.SaveChangesAsync();

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
                CountFavorite= product.CountFavorite,
                HasReceipts = product.HasReceipts,
                HasWarranty = product.HasWarranty,
            };

            await _context.Products.AddAsync( _product );
            await _context.SaveChangesAsync();

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

        /// <summary>
        /// GET for specific product and putting a view if the user is logged in
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>ProductModel</returns>
        public async Task<ProductModel?> Get(int id, int? userId)
        {
            var _data = await _context.Products.Where(p => p.ProductId == id).Select(product => new ProductModel()
            {
                ProductId = product.ProductId,
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

            if (_data == null) return null;
            if(!userId.HasValue) return _data;

            //check if the user exist in database to avoid database failure
            User? userExist = await _context.Users.FindAsync(userId);
            if (userExist == null) return _data;

            List<ProductView> list = await _context.ProductViews.Where(_p => _p.ProductId.Equals(_data.ProductId)).ToListAsync();
            bool exist = list.Any(_p => _p.UserId == userId);
            if(!exist)
            {
                _context.ProductViews.Add(new ProductView() 
                {
                    ProductId= _data.ProductId,
                    UserId = (int)userId
                });

                await _context.SaveChangesAsync();
                    
            }

            return _data;
        }

        /// <summary>
        /// DELETE a specific product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean</returns>
        public async Task<bool> DeleteProduct(int id)
        {
            Product? product = await _context.Products.FindAsync(id);

            if(product == null) return false;
            
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// PUT for specific product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Boolean</returns>
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

        //ProductComment 
        /// <summary>
        /// GET: List of comments for specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<List<ProductCommentModel>> GetProductComment(int productId)
        {
            bool product = await _context.Products.AnyAsync(_p => _p.ProductId.Equals(productId));
            if (!product) return new List<ProductCommentModel>();

            var comments = await _context.ProductComments.Where(_p => _p.ProductId == productId).Select(_p => new ProductCommentModel()
            {
                ProductCommentId= _p.ProductCommentId,
                ProductId = _p.ProductId,
                UserId= _p.UserId,
                Comment= _p.Comment,
                DateCreated = _p.DateCreated,
            }).ToListAsync();

            return comments;
        }

        /// <summary>
        /// POST: creating a comment for a specific product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public async Task<bool> AddCommentProduct(int productId, int userId, string comment)
        {
            ProductComment productComment = new ProductComment();

            productComment.ProductId = productId;
            productComment.UserId = userId;
            productComment.Comment = comment;
            productComment.DateCreated = DateTime.Now;

            await _context.ProductComments.AddAsync(productComment);

            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// DELETE: for a specific comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCommentProduct(int id)
        {
            ProductComment? _commentExist = await _context.ProductComments.FindAsync(id);
            if(_commentExist == null) return false;

            _context.Remove(_commentExist);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// PUT: updating a specific comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newComment"></param>
        /// <returns></returns>
        public async Task<bool> UpdateCommentProduct(int productId,int id, int userId,string newComment)
        {
            ProductComment? productComment = await _context.ProductComments.FindAsync(id);
            if (productComment == null) throw new IndexOutOfRangeException("Comment not found");
            if (productComment.UserId != userId) throw new ArgumentException("User is not correct");

            productComment.Comment = newComment;
            await _context.SaveChangesAsync();

            return true;
        }
        //End of Product Comment
    }
}
