using ConstradeApi.Model.MCommunity.Repository;
using ConstradeApi.Model.MProduct.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IProductRepository _prodRepo;
        private readonly ICommunityRepository _comRepo;

        public HomeController(IProductRepository prodRepo, ICommunityRepository comRepo)
        {
            _prodRepo = prodRepo;
            _comRepo = comRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchResults(string text)
        {
            try
            {
                var productResult = await _prodRepo.GetSearchProduct(text);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, new { Products =  productResult  }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("method")]
        public async Task<IActionResult> GetSearchTradeMethod(string method)
        {
            try
            {
                var products = await _prodRepo.GetSearchProductMethod(method);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
