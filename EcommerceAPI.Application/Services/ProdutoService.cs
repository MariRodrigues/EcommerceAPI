using AutoMapper;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Queries;
using System.Collections.Generic;

namespace EcommerceAPI.Application.Services
{
    public class ProdutoService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ProdutoQueries _produtoQueries;

        public ProdutoService(IMapper mapper, IProdutoRepository repository, ICategoriaRepository categoriaRepository, ISubcategoriaRepository subcategoriaRepository, ProdutoQueries produtoQueries)
        {
            _mapper = mapper;
            _produtoRepository = repository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _produtoQueries = produtoQueries;
        }

        public Produto CadastrarProduto(CreateProdutoDto produtoDto)
        {
            var subcategoria = _subcategoriaRepository.GetById(produtoDto.SubcategoriaId);
            var categoria = _categoriaRepository.GetById(subcategoria.CategoriaId);      

            if (subcategoria.Status == false 
                || categoria.Status == false)
            {
                return null;
            }

            Produto produto = _mapper.Map<Produto>(produtoDto);
            produto.CategoriaId = subcategoria.CategoriaId;

            _produtoRepository.CadastrarProduto(produto);

            return produto;
        }

        public ReadProdutoDto RecuperarProdutoPorId(int id)
        {
            var result = _produtoQueries.GetById(id);
            ReadProdutoDto produtoDto = _mapper.Map<ReadProdutoDto>(result);
            return produtoDto;
        }

        public IEnumerable<ReadProdutoDto> PesquisarProdutos(FiltrosProduto filtros)
        {
            var result = _produtoQueries.GetAllFilter(filtros);

            List<ReadProdutoDto> produtoDto = _mapper.Map<List<ReadProdutoDto>>(result);

            return produtoDto;
        }

        public ReadProdutoDto EditarProduto (int id, UpdateProdutoDto produtoDto)
        {
            var produto = _produtoQueries.GetById(id);
            var subcategoria = _subcategoriaRepository.GetById(produtoDto.SubcategoriaId);
            var categoria = _categoriaRepository.GetById(subcategoria.CategoriaId);

            if (produto != null)
            {
                _mapper.Map(produtoDto, produto);
                produto.CategoriaId = categoria.Id;
                _produtoRepository.Editar(produto); 
                var retorno = _mapper.Map<ReadProdutoDto>(produto);
                return retorno;
            }
            return null;
        }

        
    }
}