using ConstradeApi.Entity;
using ConstradeApi.Enums;
using ConstradeApi.Services.Email;
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

                await EmailService.SendOtpEmail(otp.SendTo, otp.OTP);

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<OtpResponseType> ResendOtpCode(string userValue)
        {
            OneTimePassword? _otp = await _dataContext.Otp.FirstOrDefaultAsync(_u => _u.SendTo.Equals(userValue));

            if (_otp == null) return OtpResponseType.NotFound;
            if (DateTime.UtcNow < _otp.ExpirationTime) return OtpResponseType.Active;

            string _otpValue = OtpService.GenerateOtp();

            _otp.OTP = _otpValue;
            _otp.ExpirationTime = DateTime.UtcNow.AddMinutes(5);

            await _dataContext.SaveChangesAsync();
            await EmailService.SendOtpEmail(_otp.SendTo, _otp.OTP);

            return OtpResponseType.Success;
        }

        /// <summary>
        /// GET: Verifying the otp code 
        /// </summary>
        /// <param name="userValue"></param>
        /// <param name="code"></param>
        /// <returns>True if the otp is correct and is not exceed in the expiration time or False</returns>
        public async Task<OtpResponseType> VerifyOtpCode(string userValue, string code)
        {
            OneTimePassword? _otp = await _dataContext.Otp.FirstOrDefaultAsync(_u => _u.SendTo.Equals(userValue));

            if (_otp == null) return OtpResponseType.NotFound;
            if(DateTime.UtcNow > _otp.ExpirationTime) return OtpResponseType.Expired;
            if (!_otp.OTP.Equals(code)) return OtpResponseType.WrongCode;

            _dataContext.Otp.Remove(_otp);
            await _dataContext.SaveChangesAsync();

            return OtpResponseType.Success;
        }
    }
}
