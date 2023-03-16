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

        public async Task<bool> SubmitValidId(ValidIdRequestModel info)
        {
            ValidIdRequest request = new ValidIdRequest
            {
                ValidIdNumber = info.ValidIdNumber,
                UserId = info.UserId,
                UserName = info.UserName,
                ValidationType = info.ValidationType,
                RequestDate = DateTime.Now,
                Status = "pending"
            };

            await _context.ValidIdRequests.AddAsync(request);
            await _context.SaveChangesAsync();  
            return true;
        }
    }
}
