using AutoMapper;
using ExchengeFoodAPI.Filters.City;
using ExchengeFoodAPI.Models.City;
using Infra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Mapping
{
    public class CityMapping : Profile
    {
        public CityMapping()
        {
            CreateMap<InsertCityModel, Cities>().
          ForMember(p => p.Id, opt =>
             opt.MapFrom(src => Guid.NewGuid().ToString())).
          ForMember(p => p.CreateAt, opt =>
                opt.MapFrom(src => DateTime.Now)).
          ForMember(p => p.IsActive, opt =>
                opt.MapFrom(src => true));




            CreateMap<UpdateCityModel, Cities>();
        }
    }
}
