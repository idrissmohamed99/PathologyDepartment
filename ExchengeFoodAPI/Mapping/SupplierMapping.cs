using AutoMapper;
using ExchengeFoodAPI.Models.Supplier;
using Infra.Domain;
using System;

namespace ExchengeFoodAPI.Mapping
{
    public class SupplierMapping : Profile
    {
        public SupplierMapping()
        {
            CreateMap<InsertSupplierModel, Suppliers>().
             ForMember(p => p.Id, opt =>
                opt.MapFrom(src => Guid.NewGuid().ToString())).
             ForMember(p => p.CreateAt, opt =>
                   opt.MapFrom(src => DateTime.Now)).
             ForMember(p => p.IsActive, opt =>
                   opt.MapFrom(src => true));




            CreateMap<UpdateSupplierModel, Suppliers>();
        }

    }
}
