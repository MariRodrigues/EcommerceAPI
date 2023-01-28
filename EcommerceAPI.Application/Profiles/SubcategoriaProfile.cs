using AutoMapper;
using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;

namespace EcommerceAPI.Application.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CreateSubcategoriaDto, Subcategoria>();
            CreateMap<UpdateSubcategoriaDto, Subcategoria>();
            CreateMap<UpdateSubcategoriaCommand, Subcategoria>();
            CreateMap<UpdateStatusSubcategoriaCommand, Subcategoria>();
            CreateMap<CreateSubcategoriaCommand, Subcategoria>();
            CreateMap<Subcategoria, ReadSubcategoriaDto>();
        }
    }
}
