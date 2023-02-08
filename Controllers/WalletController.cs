﻿using ConstradeApi.Entity;
using ConstradeApi.Model.MWallet;
using ConstradeApi.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace ConstradeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly DbHelper _dbHelper;

        public WalletController(DataContext context)
        {
            _dbHelper = new DbHelper(context);
        }

        // api/wallet/4
        [HttpGet("{userId}")]
        public IActionResult GetWallet(int userId)
        {
            try
            {
                WalletModel? data = _dbHelper.GetWalletUser(userId);

                if(data == null) return NotFound();

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
                bool _send = await _dbHelper.SendMoneyUser(info);
                if (!_send) return BadRequest("User Not found or Balance is insuffecient");

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, _send));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex.InnerException != null ? ex.InnerException : ex));
            }
        }

        // api/wallet/topup
        [HttpPost("topup")]
        public async Task<IActionResult> TopUpMoney([FromBody] TopUpTransactionModel info)
        {
            try
            {
                bool flag = await _dbHelper.TopUpMoney(info);

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
        public IActionResult GetAllTransactions()
        {
            try
            {
                var data = _dbHelper.GetAllMoneyTransaction();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/receive/4
        [HttpGet("transactions/receive/{uid}")]
        public IActionResult GetReceiveTransaction(int uid) 
        {
            try
            {
                var data = _dbHelper.GetReceiveMoneyTransaction(uid);

                if (data.Count == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/send/4
        [HttpGet("transactions/send/{uid}")]
        public IActionResult GetSendTransaction(int uid)
        {
            try
            {
                var data = _dbHelper.GetSendMoneyTransaction(uid);

                if (data.Count == 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/topup/wid/4
        [HttpGet("transactions/topup/wid/{walletId}")]
        public IActionResult GetTopUpTransactionByWalletId(int walletId)
        {
            try
            {
                var data = _dbHelper.GetTopUpByWalletId(walletId);

                if(data.Count== 0) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/topup/4
        [HttpGet("transactions/topup/{id}")]
        public IActionResult GetTopUpTransactionById(int id)
        {
            try
            {
                var data = _dbHelper.GetTopUpById(id);

                if (data == null) return NotFound();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // api/wallet/transactions/topup
        [HttpGet("transactions/topup")]
        public IActionResult GetAllTopUpTransaction()
        {
            try
            {
                var data = _dbHelper.GetAllTopUpTransaction();

                return Ok(ResponseHandler.GetApiResponse(ResponseType.Success, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

    }
}
