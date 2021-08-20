using Infra;
using Infra.Domain;
using Infra.DTO.CitiesDTO;
using Infra.DTOs;
using Infra.DTOs.CitiesDTO;
using Infra.Services.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CityServices : ICitiesServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> ActivateCity(string id, bool isActive)
        {
            Cities city = await _unitOfWork.GetRepository<Cities>().GetByID(id);
            city.IsActive = !isActive;
            await _unitOfWork.GetRepository<Cities>().Update(city);

            return true;
        }



        public async Task<IQueryable<Cities>> FindCityBy(Expression<Func<Cities, bool>> predicate)
        {
            return await _unitOfWork.GetRepository<Cities>().FindBy(predicate);
        }



        public async Task<List<ActivaCityDTO>> GetActiveCities(string name, int pageSize)
        {

            IQueryable<Cities> suppliers = await _unitOfWork.GetRepository<Cities>()
                                                       .FindBy(p =>
                                                                   (string.IsNullOrWhiteSpace(name) ? true : p.Name.Trim().ToLower().Contains(name.Trim().ToLower())) &&
                                                               p.IsActive);

            return await suppliers.OrderBy(p => p.Name)
                           .Select(p => new ActivaCityDTO
                           {
                               Id = p.Id,
                               Name = p.Name,
                           }).Take(pageSize).ToListAsync();
        }

        public async Task<PaginationDTO<CityDTO>> GetCities(string name, int currentPage, int pageSize)
        {
            int skip = (currentPage - 1) * pageSize;

            IQueryable<Cities> cities = await _unitOfWork.GetRepository<Cities>()
                                           .FindBy(p =>
                                                        (string.IsNullOrWhiteSpace(name) ? true : p.Name.Trim().ToLower().Contains(name.Trim().ToLower())));

            int totalCount = await cities.CountAsync();

            List<CityDTO> data = await cities.OrderByDescending(p => p.CreateAt)
                                     .Select(p => new CityDTO
                                     {
                                         Id = p.Id,
                                         Name = p.Name,
                                         IsActive = p.IsActive,

                                     }).Skip(skip).Take(pageSize).ToListAsync();

            return new PaginationDTO<CityDTO>
            {
                Data = data,
                PageCount = (totalCount - 1) / pageSize + 1,
            };
        }

        public async Task<bool> InsertCity(Cities city)
        {
            await _unitOfWork.GetRepository<Cities>().Insert(city);
            return true;
        }

        public async Task<bool> UpdateCity(Cities city)
        {
            await _unitOfWork.GetRepository<Cities>().Update(city);
            return true;
        }
    }
}
