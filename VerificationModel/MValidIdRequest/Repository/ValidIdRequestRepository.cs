using ConstradeApi.Services.EntityToModel;
using ConstradeApi.VerificationEntity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ConstradeApi.VerificationModel.MValidIdRequest.Repository
{
    public class ValidIdRequestRepository : IValidIdRequestRepository
    {
        private readonly VerificationDataContext _context;

        public ValidIdRequestRepository(VerificationDataContext context)
        {
            _context = context;
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

            ValidIdRequest _request = new ValidIdRequest
            {
                ValidIdNumber = info.ValidIdNumber,
                UserId = info.UserId,
                UserName = info.UserName,
                ValidationType = info.ValidationType,
                RequestDate = DateTime.Now,
                Status = "pending"
            };

            await _context.ValidIdRequests.AddAsync(_request);
            await _context.SaveChangesAsync();  
            return true;
        }
    }
}
