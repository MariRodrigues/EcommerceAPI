using AutoMapper;
using EcommerceAPI.Application.Commands.Produtos;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Produtos.DTO;

namespace EcommerceAPI.Application.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<UpdateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();
            CreateMap<CreateProdutoCommand, Produto>();
            CreateMap<UpdateProdutoCommand, Produto>();
        }
    }
}
