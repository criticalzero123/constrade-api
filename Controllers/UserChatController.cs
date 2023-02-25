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

        // api/<UserChatController/4?otherId=4&index=0
        [HttpGet("{currentUserId}")]
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
    }
}
