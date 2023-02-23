using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public FollowController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //POST api/<FollowController>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Follow( [FromBody] UserFollowModel userFollow)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;
                bool flag = await _userRepository.FollowUser(userFollow.FollowByUserId, userFollow.FollowedByUserId);

                if (!flag) responseType = ResponseType.Failure;

                return Ok(ResponseHandler.GetApiResponse(responseType, userFollow));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //GET api/<FollowController>/4
        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFollows(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                var followers = await _userRepository.GetUserFollower(userId);
                var follows = await _userRepository.GetUserFollow(userId);

                return Ok(ResponseHandler.GetApiResponse(responseType, new { followers, follows }));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }
    }
}
