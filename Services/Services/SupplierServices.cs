using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.DTOs.SuppliersDTO;
using Infra.Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SupplierServices : ISuppliersServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public SupplierServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ActivateSupplier(string id, bool isActive)
        {
            Suppliers supplier = await _unitOfWork.GetRepository<Suppliers>().GetByID(id);
            supplier.IsActive = !isActive;
            await _unitOfWork.GetRepository<Suppliers>().Update(supplier);

            return true;
        }




        public async Task<bool> DeleteSupplier(string id, bool isDelete)
        {
            Suppliers supplier = await _unitOfWork.GetRepository<Suppliers>().GetByID(id);
            supplier.IsDelete = !isDelete;
            await _unitOfWork.GetRepository<Suppliers>().Update(supplier);

            return true;
        }

        public async Task<IQueryable<Suppliers>> FindSupplierBy(Expression<Func<Suppliers, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<Suppliers>().FindBy(predicate);
        }

        public async Task<List<ActiveSupplierDTO>> GetActiveSuppliers(string name, int pageSize)
        {
            IQueryable<Suppliers> suppliers = await _unitOfWork.GetRepository<Suppliers>()
                                                       .FindBy(p =>
                                                                   (string.IsNullOrWhiteSpace(name) ? true : p.Name.Trim().ToLower().Contains(name.Trim().ToLower())) &&
                                                               p.IsActive && p.IsDelete == false);

            return await suppliers.OrderBy(p => p.Name)
                           .Select(p => new ActiveSupplierDTO
                           {
                               Id = p.Id,
                               Name = p.Name,
                           }).Take(pageSize).ToListAsync();
        }

        public async Task<PaginationDTO<SupplierDTO>> GetSuppliers(string name, string idNumber, bool isDelete, int currentPage, int pageSize)
        {
            int skip = (currentPage - 1) * pageSize;

            IQueryable<Suppliers> suppliers = await _unitOfWork.GetRepository<Suppliers>()
                                           .FindBy(p =>
                                                        (string.IsNullOrWhiteSpace(name) ? true : p.Name.Trim().ToLower().Contains(name.Trim().ToLower())) &&
                                                        (string.IsNullOrWhiteSpace(idNumber) ? true : p.IdNumber.Trim().ToLower().StartsWith(idNumber.Trim().ToLower())) &&
                                                         p.IsDelete == isDelete);

            int totalCount = await suppliers.CountAsync();

            List<SupplierDTO> data = await suppliers.OrderByDescending(p => p.CreateAt)
                                     .Select(p => new SupplierDTO
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         Mather = p.Mather,
                                         Gender = p.Gender,
                                         GenderName = p.Gender == 0 ? "ذكر" : "أنثى",
                                         DateOfBirth = p.DateOfBirth,
                                         CityName = p.City.Name,
                                         Phone = p.Phone,
                                         TypeOfId = p.TypeOfId,
                                         IdNumber = p.IdNumber,
                                         NationalityName = p.Nat.Name,
                                         Address = p.Address,

                                         IsActive = p.IsActive,
                                         IsDelete = p.IsDelete,
                                     }).Skip(skip).Take(pageSize).ToListAsync();

            return new PaginationDTO<SupplierDTO>
            {
                Data = data,
                PageCount = (totalCount - 1) / pageSize + 1,
            };
        }

        public async Task<bool> InsertSupplier(Suppliers suppliers)
        {
            await _unitOfWork.GetRepository<Suppliers>().Insert(suppliers);
            return true;
        }
        public async Task<bool> UpdateSupplier(Suppliers supplier)
        {
            await _unitOfWork.GetRepository<Suppliers>().Update(supplier);
            return true;
        }
    }
}
