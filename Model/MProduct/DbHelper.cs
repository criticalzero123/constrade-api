﻿using ConstradeApi.Entity;
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

        /// <summary>
        /// GET for specific product 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ProductModel</returns>
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
            bool _productExist = await _context.Products.AnyAsync(_p => _p.ProductId.Equals(productId));
            bool _userExist = await _context.Users.AnyAsync(_u => _u.User_id.Equals(userId));

            if(!_productExist) throw new IndexOutOfRangeException("Product Not found");
            if (!_userExist) throw new IndexOutOfRangeException("User Not Found");

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
        public async Task<bool> DeleteCommentProduct(int productId, int id)
        {
            bool product = await _context.Products.AnyAsync(_p => _p.ProductId.Equals(productId));
            if (!product) return false;

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
            bool product = await _context.Products.AnyAsync(_p => _p.ProductId.Equals(productId));
            if(!product) return false;

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