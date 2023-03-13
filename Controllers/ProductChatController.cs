using ConstradeApi.Entity;
using ConstradeApi.Model.MProductChat.Repository;
using ConstradeApi.Model.MProductMessage;
using ConstradeApi.Model.MProductMessage.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductChatController : ControllerBase
    {
        private readonly IProductChatRepository _productChat;
        private readonly IProductMessageRepository _productMessage;

        public ProductChatController(IProductChatRepository productChat, IProductMessageRepository productMessage)
        {
            _productChat = productChat;
            _productMessage = productMessage;
        }

        // GET api/<ProductChatController>/messages/4?userId=""&userId2=""&index=""
        [HttpGet("messages/{productId}")]
        public async Task<IActionResult> GetProductChat(int userId, int userId2, int productId, int index)
        {
            try
            {
                int? productChatId = await _productChat.GetProductChatId(userId, userId2, productId);

                if (productChatId == null) return Ok(ResponseHandler.GetApiResponse(ResponseType.NotFound, new List<ProductChat>()));

                IEnumerable<ProductMessageModel> messages = await _productMessage.GetProductMessagesByProductChatId((int)productChatId, index);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, messages));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetProductChatList(int userId)
        {
            try
            {
                var chatList = await _productChat.GetUserChatListById(userId);
            
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, chatList));

            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("messages/message/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                bool flag = await _productMessage.DeleteMessageById(id);

                if (!flag) return NotFound("message not found");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
       
    }
}
