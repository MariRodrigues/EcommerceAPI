using AutoMapper;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Categorias
{
    public class CategoriaHandler : ICategoriaHandler
    {

        private readonly CategoriaRepository _categoriaRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly SubcategoriaRepository _subcategoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaHandler(CategoriaRepository categoriaRepository, IMapper mapper, ProdutoRepository produtoRepository, SubcategoriaRepository subcategoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public async Task<ResponseApi> Handle(CreateCategoriaCommand request, CancellationToken cancellationToken)
        {
            Categoria categoria = new() { Nome = request.Nome };
            _categoriaRepository.CadastrarCategoria(categoria);

            var response = new ResponseApi(true, "Categoria cadastrada com sucesso")
            {
                Id = categoria.Id
            };

            return response;
        }

        public async Task<ResponseApi> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = _categoriaRepository.GetById(request.Id);

            if (categoria == null)
                return new ResponseApi(false, "Categoria não localizada");

            _mapper.Map(request, categoria);
            _categoriaRepository.EditarNome(categoria);

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

            if (categoria.Status == true)
            {
                if (produtos.Count != 0)
                    return new ResponseApi(false, "Há produtos cadastrados, não é possível inativar a categoria"); ;

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

            _categoriaRepository.EditarStatus(categoria);
            return new ResponseApi(true, "Status modificado com sucesso!");
        }
    }
}
