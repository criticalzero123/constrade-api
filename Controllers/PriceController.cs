using ConstradeApi.Model.Response;
using ConstradeApi.VerificationModel.MProductPrices.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PriceController : ControllerBase
    {
        private readonly IProductPricesRepository _productRepo;

        public PriceController(IProductPricesRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet("product")]
        public async Task<IActionResult> GetPrices(string query)
        {
            try
            {
                var products = await _productRepo.GetAllProductsPrice(query);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
