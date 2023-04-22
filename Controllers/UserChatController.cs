using ConstradeApi.Model.MUserChat;
using ConstradeApi.Model.MUserChat.Repository;
using ConstradeApi.Model.MUserMessage;
using ConstradeApi.Model.MUserMessage.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserChatController : ControllerBase
    {
        private readonly IUserChatRepository _userChatRepo;
        private readonly IUserMessageRepository _userMessageRepo;

        public UserChatController(IUserChatRepository userChatRepository, IUserMessageRepository userMessageRepository) {
            _userChatRepo = userChatRepository;
            _userMessageRepo = userMessageRepository;
        }

        // api/<UserChatController/messages/4?otherId=4&index=0
        [HttpGet("messages/{currentUserId}")]
        public async Task<IActionResult> GetMessages(int currentUserId, int otherId,  int index)
        {
            try
            {
                int? chatId = await _userChatRepo.GetUserChatIdByUId(currentUserId, otherId);
                if (chatId == null) return Ok(ResponseHandler.GetApiResponse(ResponseType.NotFound, new List<UserChatModel>()));

                IEnumerable<UserMessageModel> messages = await _userMessageRepo.GetMessageByUserChatId((int)chatId, index);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        // api/<UserChatController/4
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetChatListUser(int userId)
        {
            try
            {
                var messagesList = await _userChatRepo.GetUserChatListByUId(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messagesList));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/<UserChatController/4
        [HttpGet("user")]
        public async Task<IActionResult> GetByUserNameAndEmail(string username)
        {
            try
            {
                var messagesList = await _userChatRepo.GetUserByName(username);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messagesList));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("messages/message/{id}")]
        public async Task<IActionResult> DeleteMessageById(int id)
        {
            try
            {
                bool flag = await _userMessageRepo.DeleteMessageById(id);

                if (!flag) return NotFound("No Message Exist");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
