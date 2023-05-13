using ConstradeApi.Entity;
using ConstradeApi.Services.EntityToModel;
using ConstradeApi.VerificationEntity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.VerificationModel.MValidIdRequest.Repository
{
    public class ValidIdRequestRepository : IValidIdRequestRepository
    {
        private readonly VerificationDataContext _context;
        private readonly DataContext _uContext;

        public ValidIdRequestRepository(VerificationDataContext context, DataContext uContext)
        {
            _context = context;
            _uContext = uContext;
        }

        public async Task<ValidIdRequestModel?> GetValidationRequests(int userId)
        {
            ValidIdRequest? request = await _context.ValidIdRequests.Where(r => r.UserId == userId).FirstOrDefaultAsync();

            if (request == null) return null;

            return request.ToModel();
        }

        public async Task<bool> SubmitValidId(ValidIdRequestModel info)
        {
            ValidIdRequest? request = await _context.ValidIdRequests.Where(r => r.UserId == info.UserId).FirstOrDefaultAsync();

            if (request != null)
            {
                _context.ValidIdRequests.Remove(request);
                _context.SaveChanges(); 
            }

            bool exist = _context.ValidIdentification.Any(_v => _v.ValidIdNumber.Equals(info.ValidIdNumber) && _v.ValidIdType == info.ValidationType);

            ValidIdRequest _request = new ValidIdRequest
            {
                ValidIdNumber = info.ValidIdNumber,
                UserId = info.UserId,
                UserName = info.UserName,
                ValidationType = info.ValidationType,                
                RequestDate = DateTime.Now,
                Status = exist ? "accepted" : "rejected"
            };

            User? user = await _uContext.Users.FindAsync(info.UserId);
            if (user == null) return false;

            user.CountPost += 2;
            user.UserType = "verified";
            await _uContext.SaveChangesAsync();

            await _context.ValidIdRequests.AddAsync(_request);
            await _context.SaveChangesAsync();  
            return true;
        }
    }
}
