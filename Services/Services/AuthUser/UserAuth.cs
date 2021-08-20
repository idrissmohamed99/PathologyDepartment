using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.Services.IAuthUser;
using Infra.Utili;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Services.AuthUser
{
    public class UserAuth : IUserAuth
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly HelperUtili _helper;
        public UserAuth(IUnitOfWork unitOfWork, HelperUtili helper)
        {
            _unitOfWork = unitOfWork;
            _helper = helper;
        }

        public async Task<UserAuthDTO> GetInfoUser(string UserID)
        {
            UserAuthDTO result = await (await _unitOfWork.GetRepository<Users>().FindBy(user => user.Id == UserID)).
                Select(select => new UserAuthDTO
                {
                    ModuleName = select.Group.Name,
                    RoleId = select.RoleId,
                    RoleName = select.Role.Name,
                    Permisstions = select.Role.RolePermisstion.Select(select => select.Permisstion.Name).ToList()
                }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<UserAuthDTO> LoginUser(string userName, string passwprdHash)
        {
            string passHash = _helper.Hash(passwprdHash);
            UserAuthDTO result = await (await _unitOfWork.GetRepository<Users>().
                FindBy(user => user.Name == userName && user.PasswordHash == passHash)).
                Select(select => new UserAuthDTO
                {
                    Id = select.Id,
                    ModuleId = select.GroupId,
                    ModuleName = select.Group.Name,
                    Name = select.Name,
                    RoleId = select.RoleId,
                    RoleName = select.Role.Name,
                    Permisstions = select.Role.RolePermisstion.Select(select => select.Permisstion.Name).ToList()

                }).SingleOrDefaultAsync();
            return result;
        }


    }
}
