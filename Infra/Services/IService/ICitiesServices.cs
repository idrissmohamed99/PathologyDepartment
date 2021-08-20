using Infra.Domain;
using Infra.DTO.CitiesDTO;
using Infra.DTOs;
using Infra.DTOs.CitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Services.IService
{
public  interface   ICitiesServices
    {
        Task<bool> InsertCity(Cities city);
        Task<bool> UpdateCity(Cities city);

        Task<bool> ActivateCity(string id, bool isActive);
        Task<PaginationDTO<CityDTO>> GetCities(string name,  int currentPage, int pageSize);
        Task<List<ActivaCityDTO>> GetActiveCities(string name, int pageSize);
        Task<IQueryable<Cities>> FindCityBy(Expression<Func<Cities, bool>> predicate);
    }
}
