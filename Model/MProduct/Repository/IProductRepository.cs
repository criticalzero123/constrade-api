﻿
using ConstradeApi.Enums;

namespace ConstradeApi.Model.MProduct.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductAddResponseType> Save(ProductModel product, IEnumerable<string> imageList);
        Task<IEnumerable<ProductModel>> GetProductsByUserId(int userId);
        Task<ProductFullDetails?> Get(int id, int? userId);
        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateProduct(int id, ProductModel product);
        Task<bool> AddFavoriteProduct(FavoriteModel info);
        Task<bool> DeleteFavoriteProduct(int id);
        Task<IEnumerable<FavoriteModel>> GetFavoriteUser(int userId);
        Task<IEnumerable<ProductCommentModel>> GetProductComment(int productId);
        Task<bool> AddCommentProduct(int productId, int userId, string comment);
        Task<bool> DeleteCommentProduct(int id);
        Task<bool> UpdateCommentProduct(int productId, int id, int userId, string newComment);

    }
}
