using ConstradeApi.Entity;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUser.Repository;
using ConstradeApi.Model.MUserNotification.Repository;
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
        private readonly IUserNotificationRepository _userNotification;

        public FollowController(IUserRepository userRepository, IUserNotificationRepository userNotification)
        {
            _userRepository = userRepository;
            _userNotification = userNotification;
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

                await _userNotification.SendNotificationFollow(userFollow.FollowedByUserId, userFollow.FollowByUserId);

                return Ok(ResponseHandler.GetApiResponse(responseType, flag));
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

                var followCount = await _userRepository.GetUserFollowCount(userId);

                return Ok(ResponseHandler.GetApiResponse(responseType, followCount));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        [Authorize]
        [HttpGet("follower")]
        public async Task<IActionResult> GetFollowerUsers(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                var follower = await _userRepository.GetUserFollowerUsers(userId);

                return Ok(ResponseHandler.GetApiResponse(responseType, follower));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        [Authorize]
        [HttpGet("followed")]
        public async Task<IActionResult> GetFollowedUsers(int userId)
        {
            try
            {
                ResponseType responseType = ResponseType.Success;

                var followed = await _userRepository.GetUserFollowUsers(userId);

                return Ok(ResponseHandler.GetApiResponse(responseType, followed));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        //GET api/<FollowController>/4
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IsFollow(int otherUserId, int currentUserId)
        {
            try
            {
                bool flag = await _userRepository.IsFollowUser(otherUserId, currentUserId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
