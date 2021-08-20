using Infra;
using Infra.Domain;
using Infra.Utili;
using Infra.ValidationServices.IAuthUserValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.ValidationServices.AuthUserValidation
{
    public class UserAuthValidationServices : IUserAuthValidationServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;

        public UserAuthValidationServices(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }
        public async Task<Users> CheckUserIsCurrect(string userName, string passwordHash)
        {
            string passHash = _helper.Hash(passwordHash);
            Users result = await (await _unitOfWork.GetRepository<Users>().
                FindBy(user => user.Name == userName && user.PasswordHash == passHash)).SingleOrDefaultAsync();
            return result;

        }

        public async Task<bool> CheckUserIsDelete(string userId)
        {
            return await (await _unitOfWork.GetRepository<Users>().FindBy(user => user.Id == userId && user.IsDelete == true)).AnyAsync();
        }

        public async Task<bool> CheckUserIsNotActive(string userId)
        {
            return await (await _unitOfWork.GetRepository<Users>().FindBy(user => user.Id == userId && user.IsActive == false)).AnyAsync();
        }
    }
}
