﻿using ConstradeApi.Entity;
using ConstradeApi.Services.OTP;
using Microsoft.EntityFrameworkCore;

namespace ConstradeApi.Model.MOtp.Repository
{
    public class OtpRepository : IOtpRepository
    {
        private readonly DataContext _dataContext;

        public OtpRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// POST: Creating a otp code with a user email or phone
        /// </summary>
        /// <param name="userValue"></param>
        /// <returns></returns>
        public async Task<bool> GenerateOtpCode(string userValue)
        {
            try
            {
                OneTimePassword? _otp = await _dataContext.Otp.FirstOrDefaultAsync(_u => _u.SendTo.Equals(userValue));

                if (_otp != null) return true;

                string _otpValue = OtpService.GenerateOtp();

                OneTimePassword otp = new OneTimePassword()
                {
                    SendTo = userValue,
                    OTP = _otpValue,
                    ExpirationTime = DateTime.UtcNow.AddMinutes(5)
                };

                await _dataContext.Otp.AddAsync(otp);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        /// <summary>
        /// GET: Verifying the otp code 
        /// </summary>
        /// <param name="userValue"></param>
        /// <param name="code"></param>
        /// <returns>True if the otp is correct and is not exceed in the expiration time or False</returns>
        public async Task<bool> VerifyOtpCode(string userValue, string code)
        {
            OneTimePassword? _otp = await _dataContext.Otp.FirstOrDefaultAsync(_u => _u.SendTo.Equals(userValue));

            if (_otp == null || !_otp.OTP.Equals(code) || DateTime.UtcNow > _otp.ExpirationTime) return false;

            return true;
        }
    }
}
