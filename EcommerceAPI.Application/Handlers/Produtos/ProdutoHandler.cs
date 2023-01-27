using AutoMapper;
using EcommerceAPI.Application.Commands.Produtos;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Produtos
{
    public class ProdutoHandler : IProdutoHandler
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly ProdutoQueries _produtoQueries;

        public ProdutoHandler(ISubcategoriaRepository subcategoriaRepository, ICategoriaRepository categoriaRepository, IMapper mapper, IProdutoRepository produtoRepository, ProdutoQueries produtoQueries)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _produtoQueries = produtoQueries;
        }

        public async Task<ResponseApi> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            var subcategoria = _subcategoriaRepository.GetById(request.SubcategoriaId);
            var categoria = _categoriaRepository.GetById(subcategoria.CategoriaId);

            if (!subcategoria.Status
                || !categoria.Status)
            {
                return null;
            }

            Produto produto = _mapper.Map<Produto>(request);
            produto.CategoriaId = subcategoria.CategoriaId;

            _produtoRepository.CadastrarProduto(produto);

            var response = new ResponseApi(true, "Produto cadastrado com sucesso")
            {
                Id = produto.Id
            };

            return response;
        }
        public async Task<ResponseApi> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _produtoQueries.GetById(request.Id);
            var subcategoria = _subcategoriaRepository.GetById(request.SubcategoriaId);
            var categoria = _categoriaRepository.GetById(subcategoria.CategoriaId);

            ResponseApi result;

            if (produto == null)
            {
                result = new ResponseApi(false, "Produto não existe.");
                return result;
            }

            _mapper.Map(request, produto);
            produto.CategoriaId = categoria.Id;
            _produtoRepository.Editar(produto);

            result = new ResponseApi(true, "Produto atualizado com sucesso!")
            {
                Id = produto.Id
            };

            return result;
        }
    }
}
