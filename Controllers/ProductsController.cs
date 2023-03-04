using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MProduct.Repository;
using ConstradeApi.Model.MProductReport;
using ConstradeApi.Model.MProductReport.Repository;
using ConstradeApi.Model.MUserReport;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductReportRepository _productReportRepository;

        public ProductsController(IProductRepository productRepository,IProductReportRepository productReport)
        {
            _productRepository = productRepository;
            _productReportRepository = productReport;
        }

        // GET: api/<ProductController>
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseType responseType= ResponseType.Success;
                var products = await _productRepository.GetAllProducts();

                if (!products.Any()) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, products));

            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            try
            {
                var products = await _productRepository.GetProductsByUserId(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ProductController>/5
        // Get api/<ProductController>/5?uid=5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, int? uid)
        {
            try
            {
                ResponseType responseType= ResponseType.Success;

                var product = await _productRepository.Get(id,uid);

                if(product == null) responseType = ResponseType.NotFound;

                return Ok(ResponseHandler.GetApiResponse(responseType, product));

            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // POST api/<ProductController>
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductAndImages productModel)
        {
            try
            {
                ProductAddResponseType response = await _productRepository.Save(productModel.Product, productModel.ImageURLList);

                if (response == ProductAddResponseType.UserNotFound) return NotFound();
                if (response == ProductAddResponseType.NoPostCount) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{response}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, productModel));
            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // PUT api/<ProductController>/5
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductModel productModel)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                bool result = await _productRepository.UpdateProduct(id, productModel);

                if (!result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, productModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                bool _result = await _productRepository.DeleteProduct(id);

                if (!_result) return NotFound(); 

                return Ok(ResponseHandler.GetApiResponse(responseType, _result));
            }
            catch (Exception ex)
            {

                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] FavoriteModel info)
        {
            try
            {
                bool flag = await _productRepository.AddFavoriteProduct(info);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("favorite/{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            try
            {
                bool flag = await _productRepository.DeleteFavoriteProduct(id);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("favorite/{userId}")]
        public async Task<IActionResult> GetFavorites(int userId)
        {
            try
            {
                var favoriteList = await _productRepository.GetFavoriteUser(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, favoriteList));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ProductsController>/1/comment
        [HttpGet("{productId}/comment")]
        public async Task<IActionResult> GetComments(int productId)
        {
            try
            {
                ResponseType response = ResponseType.Success;

                IEnumerable<ProductCommentModel> _comments = await _productRepository.GetProductComment(productId);

                if (_comments.Count() == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(response, _comments));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<ProductsController>/1/comment
        [HttpPost("{productId}/comment")]
        public async Task<IActionResult> AddComment(int productId, [FromBody] ProductCommentModel productCommentModel)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                bool _result = await _productRepository.AddCommentProduct(productId, productCommentModel.UserId, productCommentModel.Comment);
                return Ok(ResponseHandler.GetApiResponse(responseType, productCommentModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //PUT api/<ProductsController/1/comment/1
        [HttpPut("{productId}/comment/{id}")]
        public async Task<IActionResult> UpdateComment(int productId, int id, [FromBody] ProductUpdateNewComment newMessage)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                bool _result = await _productRepository.UpdateCommentProduct(productId, id, newMessage.UserId, newMessage.NewComment);

                if (!_result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, newMessage));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<ProductsController>/1/comment/5
        [HttpDelete("{productId}/comment/{id}")]
        public async Task<IActionResult> DeleleComment( int id)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                

                bool _result = await _productRepository.DeleteCommentProduct( id);

                if(!_result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<ProductsController>/report
        [HttpPost("/report")]
        public async Task<IActionResult> ReportUser(ProductReportModel model)
        {
            try
            {
                bool flag = await _productReportRepository.ReportProduct(model);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
