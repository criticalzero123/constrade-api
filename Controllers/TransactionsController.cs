using ConstradeApi.Entity;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly DbHelper _dbHelper;
        public TransactionsController(DataContext dataContext)
        {
            _dbHelper= new DbHelper(dataContext);
        }

        // api/<TransactionsController>/product
        [HttpGet("product")]
        public IActionResult GetAllTransaction()
        {
            try
            {
                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, _dbHelper.GetAllTransaction()));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/<TransactionsController>/product/4
        [HttpGet("product/{id}")]
        public IActionResult GetTransaction(int id)
        {
            try
            {
                TransactionModel? transactionModel = _dbHelper.GetTransaction(id);

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
                bool flag = await _dbHelper.SoldProduct(transactionModel);

                if (!flag) return BadRequest("Something Went Wrong");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, transactionModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }
    }
}
