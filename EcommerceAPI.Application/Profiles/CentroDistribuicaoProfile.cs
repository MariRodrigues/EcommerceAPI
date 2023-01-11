using AutoMapper;
using EcommerceAPI.Application.Commands.Centros;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using System.Linq;

namespace EcommerceAPI.Application.Profiles
{
    public class CentroDistribuicaoProfile : Profile
    {
        public CentroDistribuicaoProfile()
        {
            CreateMap<CreateCentroDto, CentroDistribuicao>();
            CreateMap<CreateCentroCommand, CentroDistribuicao>();
            CreateMap<EnderecoJson, CentroDistribuicao>();
            CreateMap<CentroDistribuicao, ReadCentroDistribuicao>()
                .ForMember(centro => centro.Produtos, opts => opts
                .MapFrom(centro => centro.Produtos.Select
                (c => new { c.Id, c.Nome, c.Status, c.DataCriacao, c.DataModificacao, c.QuantidadeEstoque })));
            CreateMap<UpdateCentroDto, CentroDistribuicao>();
            CreateMap<UpdateCentroCommand, CentroDistribuicao>();
        }
    }
}
