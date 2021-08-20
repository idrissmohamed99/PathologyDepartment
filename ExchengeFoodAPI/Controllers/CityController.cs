using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExchengeFoodAPI.Filters.City;
using ExchengeFoodAPI.Models.City;
using Infra;
using Infra.Domain;
using Infra.DTO.CitiesDTO;
using Infra.DTOs;
using Infra.DTOs.CitiesDTO;
using Infra.Routers;
using Infra.Services.IService;
using Infra.Utili;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using Org.BouncyCastle.Asn1.Bsi;

namespace ExchengeFoodAPI.Controllers
{
    [Route(CityControllerRouter.Controller)]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICitiesServices _citiesServices;
        private readonly IMapper _mapper;
        private readonly HelperUtili _helperUtili;

        public CityController(IUnitOfWork unitOfWork, ICitiesServices citiesServices, IMapper mapper, HelperUtili helperUtili)
        {
            _unitOfWork = unitOfWork;
            _citiesServices = citiesServices;
            _mapper = mapper;
            _helperUtili = helperUtili;
        }

        [HttpPost(CityControllerRouter.InsertCity)]
        [TypeFilter(typeof(InsertCityFliter))]
        public async Task<ResultOperationDTO<bool>>InsertCity(InsertCityModel model){

            Cities city = _mapper.Map<Cities>(model);

            await _citiesServices.InsertCity(city);

            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تمت العملية بنجاح" });

        }

        [HttpPut(CityControllerRouter.UpdateCity)]
        [TypeFilter(typeof(UpdateCityFilter))]
        public async Task<ResultOperationDTO<bool>> UpdateCity(UpdateCityModel model)
        {
            Cities city = await (await _citiesServices.FindCityBy(p => p.Id == model.Id)).FirstOrDefaultAsync();


            var result = _mapper.Map(model, city);

            await _citiesServices.UpdateCity(result);


            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, new string[] { "تمت العملية بنجاح" });
        }


        [HttpPut(CityControllerRouter.ActivateCity)]
        [TypeFilter(typeof(ActivateCityFilter))]
        public async Task<ResultOperationDTO<bool>> ActivateCity(string id, bool isActive)
        {
            await _citiesServices.ActivateCity(id, isActive);

            await _unitOfWork.SaveChangeAsync();

            return ResultOperationDTO<bool>.CreateSuccsessOperation(true, message: new string[] { "تم عملية بنجاح" });
        }

        [HttpGet(CityControllerRouter.GetCitys)]
        public async Task<ResultOperationDTO<PaginationDTO<CityDTO>>> GetCities(string name,  int currentPage = 1, int pageSize = 20)
        {


            PaginationDTO<CityDTO> cities = await _citiesServices.GetCities( name, currentPage, pageSize);

            return ResultOperationDTO<PaginationDTO<CityDTO>>.CreateSuccsessOperation(cities);
        }

        [HttpGet(CityControllerRouter.GetActiveCitys)]
        public async Task<ResultOperationDTO<List<ActivaCityDTO>>> GetActiveCities(string name, int pageSize = 20)
        {

            List<ActivaCityDTO> cities = await _citiesServices.GetActiveCities(name, pageSize);

            return ResultOperationDTO<List<ActivaCityDTO>>.CreateSuccsessOperation(cities);
        }

    }
}
