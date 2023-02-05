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

        // api/<TransactionsController>
        [HttpGet]
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

        // api/<TransactionsController>/4
        [HttpGet("{id}")]
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

        // api/<TransactionController>
        [HttpPost]
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
