using AutoMapper;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Queries;
using System.Collections.Generic;

namespace EcommerceAPI.Application.Services
{
    public class ProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoQueries _produtoQueries;

        public ProdutoService(IMapper mapper, IProdutoQueries produtoQueries)
        {
            _mapper = mapper;
            _produtoQueries = produtoQueries;
        }

        public IEnumerable<ReadProdutoDto> PesquisarProdutos(FiltrosProduto filtros)
        {
            var result = _produtoQueries.GetAllFilter(filtros);
            List<ReadProdutoDto> produtoDto = _mapper.Map<List<ReadProdutoDto>>(result);

            return produtoDto;
        }
    }
}