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

        //public async Task<IEnumerable<GetRequestAdmin>> GetValidationRequests()
        //{
        //    var requests = await _context.ValidIdRequests.Select(_r => new GetRequestAdmin
        //    {
        //        RequestInfo = _r.ToModel(),
        //        Exist = _context.ValidIdentification.Any(_v => _v.Equals(_r.ValidIdNumber) && _v.ValidIdType == _r.ValidationType),
        //    }).ToListAsync();

        //    return requests;
        //}

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
