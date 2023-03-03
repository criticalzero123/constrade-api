using ConstradeApi.Entity;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.MTransaction.Repository;
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
        public TransactionsController(ITransactionRepository transactionRespository)
        {
            _transactionRespository= transactionRespository;
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
                TransactionModel? transactionModel = await _transactionRespository.GetTransaction(id);

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
                bool flag = await _transactionRespository.SoldProduct(transactionModel);

                if (!flag) return BadRequest("Something Went Wrong");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }
    }
}
