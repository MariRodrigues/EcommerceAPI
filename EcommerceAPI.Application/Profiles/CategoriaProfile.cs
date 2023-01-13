using AutoMapper;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Categorias.DTO;
using System.Linq;

namespace EcommerceAPI.Application.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<UpdateCategoriaCommand, Categoria>();
            CreateMap<CreateCategoriaCommand, Categoria>();
        }
        
    }
}
