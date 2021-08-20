using Infra.Services.IService;
using Infra.ValidationServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ValidationServices
{
    public class CityValidationServices : ICityValidationServices
    {
        private readonly ICitiesServices _citiesServices;

        public CityValidationServices(ICitiesServices citiesServices)
        {
            _citiesServices = citiesServices;
        }
   
        public async Task<bool> IsNameExist(string name)
        {
            return await (await _citiesServices.FindCityBy(p => p.Name.Trim().ToLower() == name.Trim().ToLower())).AnyAsync();
        }

        public async Task<bool> IsNameExist(string id, string name)
        {
            return await (await _citiesServices.FindCityBy(p => p.Name.Trim().ToLower() == name.Trim().ToLower())).AnyAsync();
        }
    }
}
