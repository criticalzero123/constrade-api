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
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly IUserNotificationRepository _nofication;
        private readonly IUserRepository _userRepository;

        public NotificationController(IUserNotificationRepository repo, IUserRepository userRepo)
        {
            _nofication = repo;
            _userRepository = userRepo;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotificationsByUser(int userId)
        {
            try
            {
                var get = await _nofication.GetNotifications(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, get));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("count/{userId}")]
        public async Task<IActionResult> GetNotificationCount(int userId)
        {
            try
            {
                var count = await _userRepository.GetUnreadNotif(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, count));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("read")]
        public async Task<IActionResult> MarkReadNotif(int id)
        {
            try
            {
                var flag = await _userRepository.MarkAsReadNotif(id);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
