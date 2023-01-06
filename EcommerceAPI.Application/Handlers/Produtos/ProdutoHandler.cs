﻿using AutoMapper;
using EcommerceAPI.Application.Commands.Produtos;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Produtos
{
    public class ProdutoHandler : IProdutoHandler
    {
        private readonly SubcategoriaRepository _subcategoriaRepository;
        private readonly CategoriaRepository _categoriaRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoHandler(SubcategoriaRepository subcategoriaRepository, CategoriaRepository categoriaRepository, IMapper mapper, ProdutoRepository produtoRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
            _produtoRepository = produtoRepository;
        }

        public async Task<ResponseApi> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            var subcategoria = _subcategoriaRepository.GetById(request.SubcategoriaId);
            var categoria = _categoriaRepository.GetById(subcategoria.CategoriaId);

            if (subcategoria.Status == false
                || categoria.Status == false)
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
            var produto = _produtoRepository.GetById(request.Id);
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