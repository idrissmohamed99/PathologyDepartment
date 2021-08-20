using Infra.Domain;
using Infra.DTOs;
using Infra.DTOs.SuppliersDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infra.Services.IService
{
    public interface ISuppliersServices
    {
        Task<bool> InsertSupplier(Suppliers supplier);
        Task<bool> UpdateSupplier(Suppliers supplier);

        Task<bool> ActivateSupplier(string id, bool isActive);
        Task<bool> DeleteSupplier(string id, bool isDelete);
        Task<PaginationDTO<SupplierDTO>> GetSuppliers(string name, string idNumber, bool isDelete, int currentPage, int pageSize);
        Task<List<ActiveSupplierDTO>> GetActiveSuppliers(string name, int pageSize);
        Task<IQueryable<Suppliers>> FindSupplierBy(Expression<Func<Suppliers, bool>> predicate);

    }
}
