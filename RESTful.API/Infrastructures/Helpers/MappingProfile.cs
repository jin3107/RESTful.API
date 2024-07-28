using AutoMapper;
using RESTful.API.DTOs;
using RESTful.API.Models.Entity;

namespace RESTful.API.Infrastructures.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
