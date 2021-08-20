using System.Threading.Tasks;

namespace Infra.ValidationServices
{
    public interface ISupplierValidationServices
    {
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExist(string id, string name);
        Task<bool> IsIdNumExist(string idNumber);
        Task<bool> IsIdNumExist(string id, string idNumber);
        Task<bool> IsArchive(string id);
    }
}
