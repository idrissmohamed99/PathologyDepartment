using Infra.Services.IService;
using Infra.ValidationServices;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Services.ValidationServices
{
    public class SupplierValidationServices : ISupplierValidationServices
    {
        private readonly ISuppliersServices _suppliersServices;

        public SupplierValidationServices(ISuppliersServices suppliersServices)
        {
            _suppliersServices = suppliersServices;
        }

        public async Task<bool> IsNameExist(string name)
        {
            return await (await _suppliersServices.FindSupplierBy(p => p.Name.Trim().ToLower() == name.Trim().ToLower())).AnyAsync();
        }

        public async Task<bool> IsNameExist(string id, string name)
        {
            return await (await _suppliersServices.FindSupplierBy(p => p.Name.Trim().ToLower() == name.Trim().ToLower())).AnyAsync();
        }

        public async Task<bool> IsIdNumExist(string idNumber)
        {
            return await (await _suppliersServices.FindSupplierBy(p => p.IdNumber.Trim().ToLower() == idNumber.Trim().ToLower())).AnyAsync();
        }

        public async Task<bool> IsIdNumExist(string id, string idNumber)
        {
            return await (await _suppliersServices.FindSupplierBy(p => p.Id != id && p.IdNumber.Trim().ToLower() == idNumber.Trim().ToLower())).AnyAsync();
        }

        public async Task<bool> IsArchive(string id)
        {
            return await (await _suppliersServices.FindSupplierBy(p => p.Id == id && p.IsDelete)).AnyAsync();
        }
    }
}
