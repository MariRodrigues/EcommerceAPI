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
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<UpdateCategoriaDto, Categoria>();
            CreateMap<Categoria, ReadCategoriaDto>()
                .ForMember(categoria => categoria.Subcategorias, opts => opts
                .MapFrom(categoria => categoria.Subcategorias.Select
                (c => new { c.Id, c.Nome, c.Status })))
                .ForMember(categoria => categoria.Produtos, opts => opts
                .MapFrom(categoria => categoria.Produtos.Select
                (c => new { c.Id, c.Nome, c.Status, c.CentroDistribuicao, c.DataCriacao })));
            CreateMap<UpdateCategoriaCommand, Categoria>();
        }
        
    }
}
