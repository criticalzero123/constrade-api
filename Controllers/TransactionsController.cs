using ConstradeApi.Entity;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.MTransaction.Repository;
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
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRespository;
        private readonly IUserNotificationRepository _notif;

        public TransactionsController(ITransactionRepository transactionRespository, IUserNotificationRepository notif)
        {
            _transactionRespository= transactionRespository;
            _notif = notif;
        }

        // api/<TransactionsController>/product
        [HttpGet("product")]
        public async Task<IActionResult> GetAllTransaction()
        {
            try
            {
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, await _transactionRespository.GetAllTransaction()));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/<TransactionsController>/product/4
        [HttpGet("product/{id}")]
        public async Task<IActionResult> GetTransaction(int id)
        {
            try
            {
                var transactionModel = await _transactionRespository.GetTransaction(id);

                if (transactionModel == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, transactionModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/<TransactionController>/product
        [HttpPost("product")]
        public async Task<IActionResult> SoldProduct([FromBody] TransactionModel transactionModel)
        {
            try
            {
                int flag = await _transactionRespository.SoldProduct(transactionModel);

                if (flag == -1) return BadRequest("Something Went Wrong");

                await _notif.SendNotificationTransaction(transactionModel.SellerUserId, transactionModel.BuyerUserId, flag);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // api/<TransactionController>/product
        [HttpGet("users/{uid}")]
        public async Task<IActionResult> GetTransacionUser(int uid)
        {
            try
            {
                var transactions = await _transactionRespository.GetTransactionByUser(uid);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, transactions));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }
    }
}
