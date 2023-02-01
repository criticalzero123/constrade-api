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
                IEnumerable<ProductModel> products = _dbHelper.GetProducts();

                if(!products.Any()) 
                {
                    responseType = ResponseType.NotFound;
                }

                return Ok(ResponseHandler.GetApiResponse(responseType, products));

            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel productModel)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                _dbHelper.Save(productModel);

                return Ok(ResponseHandler.GetApiResponse(responseType, productModel));
            }catch(Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
