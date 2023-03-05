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

        public NotificationController(IUserNotificationRepository repo)
        {
            _nofication = repo;
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
    }
}
