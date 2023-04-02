using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Model.MWallet;
using ConstradeApi.Model.MWallet.Repository;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalletController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;

        public WalletController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        // api/wallet/4
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWallet(int userId)
        {
            try
            {
                WalletModel? data = await _walletRepository.GetWalletUser(userId);

                if(data == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("user/all")]
        public async Task<IActionResult> GetAllUserWalletDetails()
        {
            try
            {
                var data = await _walletRepository.GetAllWalletUserDetails();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
                
            }
        }

        // api/wallet/id/4
        [HttpGet("id/{walletId}")]
        public async Task<IActionResult> GetWalletById(int walletId)
        {
            try
            {
                WalletModel? data = await _walletRepository.GetWalletById(walletId);

                if (data == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/send
        [HttpPost("send")]
        public async Task<IActionResult> SendMoney([FromBody] SendMoneyTransactionModel info)
        {
            try
            {
                WalletResponseType _send = await _walletRepository.SendMoneyUser(info);

                if (_send == WalletResponseType.UserNotFound) return Ok(ResponseHandler.GetApiResponse(ResponseType.NotFound, $"{_send}"));
                if (_send == WalletResponseType.Error) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{_send}"));
                if (_send == WalletResponseType.NotEnough) return Ok(ResponseHandler.GetApiResponse(ResponseType.Failure, $"{_send}"));



                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, $"{_send}"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // api/wallet/topup
        [HttpPost("topup")]
        public async Task<IActionResult> TopUpMoney([FromBody] OtherTransactionModel info)
        {
            try
            {
                bool flag = await _walletRepository.TopUpMoney(info);

                if (!flag) return BadRequest("Wallet Not Found");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, flag));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // api/wallet/transactions
        [HttpGet("transactions/all")]
        public async  Task<IActionResult> GetAllTransactions()
        {
            try
            {
                var data = await _walletRepository.GetAllMoneyTransaction();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/all/4
        [HttpGet("transactions/all/{userId}")]
        public async Task<IActionResult> GetWalletAllTransaction(int userId)
        {
            try
            {
                var data = await _walletRepository.GetTransactionWalletPartial(userId);

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/receive/4
        [HttpGet("transactions/receive/{walletId}")]
        public async Task<IActionResult> GetReceiveTransaction(int walletId) 
        {
            try
            {
                var data = await _walletRepository.GetReceiveMoneyTransaction(walletId);

                if (data.Count() == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/send/4
        [HttpGet("transactions/send/{walletId}")]
        public async Task<IActionResult> GetSendTransaction(int walletId)
        {
            try
            {
                var data = await _walletRepository.GetSendMoneyTransaction(walletId);

                if (data.Count() == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("transactions/id/{id}")]
        public async Task<IActionResult> GetTransactionByWalletId(int id)
        {
            try
            {
                SendMoneyTransactionModel? data = await _walletRepository.GetWalletTransactionById(id);

                if (data == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/topup/wid/4
        [HttpGet("transactions/topup/wid/{walletId}")]
        public async Task<IActionResult> GetTopUpTransactionByWalletId(int walletId)
        {
            try
            {
                var data = await _walletRepository.GetOtherTransactions(walletId);

                if(data.Count() == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        
    }
}
