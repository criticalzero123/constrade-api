using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MUser;
using ConstradeApi.Services.EntityToModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConstradeApi.Model.MProduct.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  GET All
        /// </summary>
        /// <returns>List of Products</returns>
        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            List<ProductModel> _products = new List<ProductModel>();

            List<Product> data = await _context.Products.ToListAsync();

            data.ForEach(row => _products.Add(row.ToModel()));

            return _products;
        }

        /// <summary>
        ///  GET: Getting all the product posted by user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of Products</returns>
        public async Task<IEnumerable<ProductCardDetails>> GetProductsByUserId(int userId)
        {
            IEnumerable<ProductCardDetails> products = await _context.Products.Include(_p => _p.User.Person)
                                                                        .Where(_p => _p.PosterUserId == userId)
                                                                        .OrderBy(_p => _p.DateCreated)
                                                                        .Select(_p => new ProductCardDetails
                                                                        {
                                                                            ProductId = _p.ProductId,
                                                                            ProductName = _p.Title,
                                                                            ThumbnailUrl = _p.ThumbnailUrl,
                                                                            UserName = _p.User.Person.FirstName + " " + _p.User.Person.LastName,
                                                                            UserImage = _p.User.ImageUrl,
                                                                            PreferTrade = _p.PreferTrade,
                                                                            DateCreated = _p.DateCreated,

                                                                        })
                                                                        .ToListAsync();

            return products;
        }

        /// <summary>
        /// POST for product
        /// </summary>
        /// <param name="product"></param>
        public async Task<CreationProductResponse> Save(ProductModel product, IEnumerable<string> imageList)
        {
            User? user = await _context.Users.FindAsync(product.PosterUserId);
            if (user == null) return new CreationProductResponse 
            { 
                Response = ProductAddResponseType.UserNotFound,
                ProductId = 0,
                PosterUserId = 0
            };
            if (user.UserType == "semi-verified") return new CreationProductResponse
            {
                Response = ProductAddResponseType.NotVerified,
                ProductId = 0,
                PosterUserId = 0,
            };
            //Prevent the user that less than 1 count post to post a product
            if (user.CountPost < 1) return new CreationProductResponse 
            { 
                Response = ProductAddResponseType.NoPostCount,
                ProductId = 0,
                PosterUserId = 0
            };

            user.CountPost -= 1;
            await _context.SaveChangesAsync();

            Product _product = new Product()
            {
                PosterUserId = product.PosterUserId,
                Title = product.Title,
                Description = product.Description,
                Condition = product.Condition,
                PreferTrade = product.PreferTrade,
                IsMeetup = product.IsMeetup,
                IsDeliver = product.IsDeliver,
                ProductStatus = product.ProductStatus,
                Location = product.Location,
                ModelNumber = product.ModelNumber,
                SerialNumber = product.SerialNumber,
                GameGenre = product.GameGenre,
                Platform = product.Platform,
                ThumbnailUrl = product.ThumbnailUrl,
                Cash = product.Cash,
                Item = product.Item,
                CountFavorite = product.CountFavorite,
                HasReceipts = product.HasReceipts,
                Value = product.Value,
                HasWarranty = product.HasWarranty,
                IsGenerated= product.IsGenerated,
            };

            await _context.Products.AddAsync(_product);
            await _context.SaveChangesAsync();

            // Adding the images in image list table
            foreach (string item in imageList)
            {
                _context.Images.Add(new ImageList()
                {
                    ProductId = _product.ProductId,
                    ImageURL = item
                });
            }

            _context.SaveChanges();

            return new CreationProductResponse 
            { 
                Response = ProductAddResponseType.Success, 
                ProductId = _product.ProductId, 
                PosterUserId = product.PosterUserId
            };
        }

        /// <summary>
        /// GET for specific product and add the product view count if the user is logged in
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>ProductModel</returns>
        public async Task<ProductFullDetails?> Get(int id, int? userId)
        {
            var _data = _context.Products.Include(_p => _p.User.Person).ToList().Where(_p => _p.ProductId == id)
                                                .GroupJoin(_context.Images.ToList(),
                                                           _p => _p.ProductId,
                                                           _i => _i.ProductId,
                                                           (_p , _i) => new 
                                                           {
                                                               Product = _p,
                                                               Images = _i,
                                                           })
                                                .Select(product => new ProductFullDetails
                                                {
                                                    User = product.Product.User.ToModel(),
                                                    Product = product.Product.ToModel(),
                                                    Person = product.Product.User.Person.ToModel(),
                                                    Images = product.Images.Select(_i => _i.ToModel()),
                                                    IsFavorite = _context.ProductFavorite.Any(_pf => _pf.ProductId == id && _pf.UserId == userId),
                                                    IsBoosted = _context.BoostProduct.Any(bp => bp.ProductId == id && bp.Status == "active" ),
                                                }).FirstOrDefault();

            if (_data == null) return null;
            if (!userId.HasValue) return _data;

            //check if the user exist in database to avoid database failure
            User? userExist = await _context.Users.FindAsync(userId);
            if (userExist == null) return _data;

            List<ProductView> list = await _context.ProductViews.Where(_p => _p.ProductId.Equals(_data.Product.ProductId)).ToListAsync();
            bool exist = list.Any(_p => _p.UserId == userId);
            if (!exist)
            {
                _context.ProductViews.Add(new ProductView()
                {
                    ProductId = _data.Product.ProductId,
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

            if (product == null) return false;

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
            if (_product == null) return false;

            _product.Title = product.Title;
            _product.Description = product.Description;
            _product.ModelNumber = product.ModelNumber;
            _product.SerialNumber = product.SerialNumber;
            _product.GameGenre = product.GameGenre;
            _product.Platform = product.Platform;
            _product.ThumbnailUrl = product.ThumbnailUrl;
            _product.Cash = product.Cash;
            _product.Item = product.Item;
            _product.Location = product.Location;
            _product.Condition = product.Condition;
            _product.PreferTrade = product.PreferTrade;
            _product.ThumbnailUrl = product.ThumbnailUrl;
            _product.IsDeliver = product.IsDeliver;
            _product.IsMeetup = product.IsMeetup;


            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddFavoriteProduct(FavoriteModel info)
        {
            Favorites? _info = await _context.ProductFavorite.Where(_f => _f.UserId == info.UserId &&
                                                                      _f.ProductId == info.ProductId)
                                                        .FirstOrDefaultAsync();
            Product? product = await _context.Products.FindAsync(info.ProductId);

            if (product == null) return false;

            if(_info == null)
            {
                Favorites favorite = new Favorites
                {
                    ProductId = info.ProductId,
                    UserId = info.UserId,
                    Date = DateTime.Now,
                };
                product.CountFavorite += 1;
                await _context.ProductFavorite.AddAsync(favorite);

                await _context.SaveChangesAsync();
                return true;
            } 
            else
            {
                product.CountFavorite -= 1;
                _context.ProductFavorite.Remove(_info);
                await _context.SaveChangesAsync();
                return false;
            }

        }

        public async Task<bool> DeleteFavoriteProduct(int id)
        {
            Favorites? favorite = await _context.ProductFavorite.FindAsync(id);
            
            if (favorite == null) return false;
            Product? product = await _context.Products.FindAsync(favorite.ProductId);

            if(product == null) return false;

            product.CountFavorite -= 1;
            _context.ProductFavorite.Remove(favorite);
            await _context.SaveChangesAsync();

            return true;

        }

        public async Task<IEnumerable<FavoriteProductDetails>> GetFavoriteUser(int userId)
        {
            IEnumerable<FavoriteProductDetails> favorites = await _context.ProductFavorite.Include(_f => _f.Product.User.Person)
                                                                                .Where(_u => _u.UserId == userId)
                                                                                .Select(_f => new FavoriteProductDetails
                                                                                {
                                                                                    FavoriteId = _f.FavoriteId,
                                                                                    Product = new ProductCardDetails
                                                                                    {
                                                                                        ProductId = _f.ProductId,
                                                                                        ProductName = _f.Product.Title,
                                                                                        ThumbnailUrl = _f.Product.ThumbnailUrl,
                                                                                        UserName = _f.User.Person.FirstName + " " + _f.User.Person.LastName,
                                                                                        UserImage = _f.User.ImageUrl,
                                                                                        DateCreated = _f.Product.DateCreated,
                                                                                        PreferTrade = _f.Product.PreferTrade,
                                                                                    }
                                                                                }).ToListAsync();

            return favorites;
        }

        public async Task<IEnumerable<ProductCardDetails>> GetSearchProduct(string text)
        {
            IEnumerable<ProductCardDetails> products = await _context.Products.Include(_p => _p.User.Person)
                                                                                .Where(_p => _p.Title.ToLower().Contains(text.ToLower()))
                                                                                .OrderBy(_p => _p.CountFavorite)
                                                                                .Select(_p => new ProductCardDetails
                                                                                {
                                                                                    ProductId = _p.ProductId,
                                                                                    ProductName = _p.Title,
                                                                                    ThumbnailUrl = _p.ThumbnailUrl,
                                                                                    UserName = _p.User.Person.FirstName + " " + _p.User.Person.LastName,
                                                                                    UserImage = _p.User.ImageUrl,
                                                                                    PreferTrade = _p.PreferTrade,
                                                                                    DateCreated = _p.DateCreated,

                                                                                })
                                                                                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<ProductCardDetails>> GetSearchProductMethod(string tradeMethod)
        {
            IEnumerable<ProductCardDetails> products = await _context.Products.Include(_p => _p.User.Person)
                                                                              .Where(p => p.PreferTrade == tradeMethod)
                                                                              .OrderBy(_p => _p.CountFavorite)
                                                                                .Select(_p => new ProductCardDetails
                                                                                {
                                                                                    ProductId = _p.ProductId,
                                                                                    ProductName = _p.Title,
                                                                                    ThumbnailUrl = _p.ThumbnailUrl,
                                                                                    UserName = _p.User.Person.FirstName + " " + _p.User.Person.LastName,
                                                                                    UserImage = _p.User.ImageUrl,
                                                                                    PreferTrade = _p.PreferTrade,
                                                                                    DateCreated = _p.DateCreated,

                                                                                })
                                                                                .ToListAsync(); 

            return products;
        }

        public async Task<IEnumerable<ProductCardDetails>> GetProductByLength(int count)
        {
            IEnumerable<ProductCardDetails> products = await _context.Products.Include(_p => _p.User.Person)
                                                                              .OrderBy(_p => _p.CountFavorite)
                                                                                .Select(_p => new ProductCardDetails
                                                                                {
                                                                                    ProductId = _p.ProductId,
                                                                                    ProductName = _p.Title,
                                                                                    ThumbnailUrl = _p.ThumbnailUrl,
                                                                                    UserName = _p.User.Person.FirstName + " " + _p.User.Person.LastName,
                                                                                    UserImage = _p.User.ImageUrl,
                                                                                    PreferTrade = _p.PreferTrade,
                                                                                    DateCreated = _p.DateCreated,

                                                                                })
                                                                                .Take(count)
                                                                                .ToListAsync();

            return products;
        }

        public Task<IEnumerable<ProductCardDetails>> GetProductByFollow(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
