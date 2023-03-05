using ConstradeApi.Enums;
using ConstradeApi.Model.MCommunity;
using ConstradeApi.Model.MCommunity.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityRepository _communityRepo;

        public CommunityController(ICommunityRepository communityRepo)
        {
            _communityRepo = communityRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostCreateCommunity([FromBody] CommunityModel info)
        {
            try
            {
                CommunityResponse response = await _communityRepo.CreateCommunity(info);

                if (response == CommunityResponse.NotVerified) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{response}"));

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{response}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex)); ;
            }
        }

        [HttpGet("created/{userId}")]
        public async Task<IActionResult> GetMyCommunity(int userId)
        {
            try
            {
                var list = await _communityRepo.GetCommunityByOwnerId(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, list));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommunity()
        {
            try
            {
                var list = await _communityRepo.GetCommunities();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, list));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
