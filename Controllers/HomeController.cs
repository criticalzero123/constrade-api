using ConstradeApi.Model.MBoostProduct.Repository;
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
        private readonly IBoostProductRepository _boostRepo;
        private readonly ICommunityRepository _commuRepo;

        public HomeController(IProductRepository prodRepo, ICommunityRepository comRepo, IBoostProductRepository boostProd, ICommunityRepository commuRepo)
        {
            _prodRepo = prodRepo;
            _comRepo = comRepo;
            _boostRepo = boostProd;
            _commuRepo = commuRepo;
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

        [HttpGet("boosted")]
        public async Task<IActionResult> GetBoostedProduct()
        {
            try
            {
                var products = await _boostRepo.GetBoostedProducts();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("genre")]
        public async Task<IActionResult> GetGenreProduct(string genre)
        {
            try
            {
                var products = await _prodRepo.GetSearchProductGenre(genre);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("platform")]
        public async Task<IActionResult> GetPlatformProduct(string platform)
        {
            try
            {
                var products = await _prodRepo.GetSearchProductPlatform(platform);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetProductPopularByLength(int count)
        {
            try
            {
                var products = await _prodRepo.GetProductByLength(count);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, products));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("community")]
        public async Task<IActionResult> GetCommunityPopular(int userId)
        {
            try
            {
                var community = await _commuRepo.GetPopularCommunity(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, community));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
