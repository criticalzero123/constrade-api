using ConstradeApi.Entity;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbHelper _dbHelper;

        public ProductsController(DataContext dataContext)
        {
            _dbHelper = new DbHelper(dataContext);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                ResponseType responseType= ResponseType.Success;
                List<ProductModel> products = _dbHelper.GetAllProducts();

                if (!products.Any()) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, products));

            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                ResponseType responseType= ResponseType.Success;

                var product = await _dbHelper.Get(id);

                if(product == null) responseType = ResponseType.NotFound;

                return Ok(ResponseHandler.GetApiResponse(responseType, product));

            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductAndImages productModel)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _dbHelper.Save(productModel.Product, productModel.ImageURLList);

                return Ok(ResponseHandler.GetApiResponse(responseType, productModel));
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

                bool result = await _dbHelper.UpdateProduct(id, productModel);

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
                bool _result = await _dbHelper.DeleteProduct(id);

                if (!_result) return NotFound(); 

                return Ok(ResponseHandler.GetApiResponse(responseType, id));
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

                List<ProductCommentModel> _comments = await _dbHelper.GetProductComment(productId);

                if (_comments.Count == 0) return NotFound();

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

                bool _result = await _dbHelper.AddCommentProduct(productId, productCommentModel.UserId, productCommentModel.Comment);
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

                bool _result = await _dbHelper.UpdateCommentProduct(productId, id, newMessage.UserId, newMessage.NewComment);

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
                

                bool _result = await _dbHelper.DeleteCommentProduct( id);

                if(!_result) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(responseType, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
