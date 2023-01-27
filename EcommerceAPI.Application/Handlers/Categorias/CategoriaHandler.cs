using AutoMapper;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Categorias
{
    public class CategoriaHandler : ICategoriaHandler
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaHandler(
            ICategoriaRepository categoriaRepository, 
            IMapper mapper, 
            IProdutoRepository produtoRepository, 
            ISubcategoriaRepository subcategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<ResponseApi> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            Categoria categoria = _mapper.Map<Categoria>(request);
            var response = _categoriaRepository.CadastrarCategoria(categoria);

            if (response == null)
                return new ResponseApi(false, "Não foi possível cadastrar a categoria.");               
            
            return new ResponseApi(true, "Categoria cadastrada com sucesso")
            {
                Id = categoria.Id
            };
        }

        public async Task<ResponseApi> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = _categoriaRepository.GetById(request.Id);

            if (categoria == null)
                return new ResponseApi(false, "Categoria não localizada");

            _mapper.Map(request, categoria);
            _categoriaRepository.EditarCategoria(categoria);

            var response = new ResponseApi(true, "Categoria atualizada com sucesso")
            {
                Id = categoria.Id
            };

            return response;
        }

        public async Task<ResponseApi> Handle(UpdateStatusCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = _categoriaRepository.GetById(request.Id);

            if (categoria == null)
                return new ResponseApi(false, "Categoria não localizada");

            var produtos = _produtoRepository.GetAll().Where(p => p.CategoriaId == categoria.Id).ToList();

            if (categoria.Status)
            {
                if (produtos.Count != 0)
                    return new ResponseApi(false, "Há produtos cadastrados, não é possível inativar a categoria");

                categoria.Status = false;
                categoria.DataModificacao = DateTime.Now;

                var subcategorias = _subcategoriaRepository.GetAll().Where(p => p.CategoriaId == categoria.Id).ToList();

                foreach (var subcategoria in subcategorias)
                {
                    subcategoria.Status = false;
                    subcategoria.DataModificacao = DateTime.Now;
                }
            }
            else
            {
                categoria.Status = true;
                categoria.DataModificacao = DateTime.Now;

                var subcategorias = _subcategoriaRepository.GetAll().Where(p => p.CategoriaId == categoria.Id).ToList();

                foreach (var subcategoria in subcategorias)
                {
                    subcategoria.Status = true;
                    subcategoria.DataModificacao = DateTime.Now;
                }
            }

            _categoriaRepository.EditarCategoria(categoria);
            return new ResponseApi(true, "Status modificado com sucesso!");
        }
    }
}
