using AutoMapper;
using ExchengeFoodAPI.Filters.Supplier;
using ExchengeFoodAPI.Models.Supplier;
using Infra;
using Infra.Domain;
using Infra.DTOs;
using Infra.DTOs.SuppliersDTO;
using Infra.Routers;
using Infra.Services.IService;
using Infra.Utili;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Controllers
{
    [Route(SupplierControllerRouter.Controller)]

    public class SupplierController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISuppliersServices _suppliersServices;
        private readonly IMapper _mapper;

        private readonly HelperUtili _helperUtili;
        public SupplierController(IUnitOfWork unitOfWork, ISuppliersServices suppliersServices, IMapper mapper, HelperUtili helperUtili)
        {
            _unitOfWork = unitOfWork;
            _suppliersServices = suppliersServices;
            _mapper = mapper;
            _helperUtili = helperUtili;
        }


        [HttpPost(SupplierControllerRouter.InsertSupplier)]
        [TypeFilter(typeof(InsertSupplierFilter))]
        public async Task<ResultOperationDTO<bool>> InsertSupplier(InsertSupplierModel model)
        {
            Suppliers supplier = _mapper.Map<Suppliers>(model);

            await _suppliersServices.InsertSupplier(supplier);


            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تمت العملية بنجاح" });
        }

        [HttpPut(SupplierControllerRouter.UpdateSupplier)]
        [TypeFilter(typeof(UpdateSuppilerFilter))]
        public async Task<ResultOperationDTO<bool>> UpdateSupplier(UpdateSupplierModel model)
        {
            Suppliers supplier = await (await _suppliersServices.FindSupplierBy(p => p.Id == model.Id)).FirstOrDefaultAsync();

           
            var result = _mapper.Map(model, supplier);

            await _suppliersServices.UpdateSupplier(result);


            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تمت العملية بنجاح" });
        }

        [HttpPut(SupplierControllerRouter.ActivateSupplier)]
        [TypeFilter(typeof(ActivateSupplierFilter))]
        public async Task<ResultOperationDTO<bool>> ActivateSupplier(string id, bool isActive)
        {
            await _suppliersServices.ActivateSupplier(id, isActive);

            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, message: new string[] { "تم عملية بنجاح" });
        }

        [HttpDelete(SupplierControllerRouter.DeleteSupplier)]
        [TypeFilter(typeof(DeleteSupplierFilter))]
        public async Task<ResultOperationDTO<bool>> DeleteSupplier(string id, bool isDelete)
        {
            await _suppliersServices.DeleteSupplier(id, isDelete);

            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تمت العملية بنجاح" });
        }

        [HttpGet(SupplierControllerRouter.GetSuppliers)]
        public async Task<ResultOperationDTO<PaginationDTO<SupplierDTO>>> GetSuppliers(string name, string idNumber
                                                                                                 , bool isDelete, int currentPage = 1, int pageSize = 20)
        {


            PaginationDTO<SupplierDTO> suppliers = await _suppliersServices.GetSuppliers(name, idNumber, isDelete, currentPage, pageSize);

            return ResultOperationDTO<PaginationDTO<SupplierDTO>>.CreateSuccsessOperation(suppliers);
        }

        [HttpGet(SupplierControllerRouter.GetActiveSuppliers)]
        public async Task<ResultOperationDTO<List<ActiveSupplierDTO>>> GetActiveSuppliers(string name, int pageSize = 20)
        {

            List<ActiveSupplierDTO> suppliers = await _suppliersServices.GetActiveSuppliers(name, pageSize);

            return ResultOperationDTO<List<ActiveSupplierDTO>>.CreateSuccsessOperation(suppliers);
        }

    }

}

