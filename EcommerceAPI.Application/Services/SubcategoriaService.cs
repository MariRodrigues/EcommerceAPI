using AutoMapper;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Infra.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Application.Services
{
    public class SubcategoriaService
    {
        private readonly IMapper _mapper;
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public SubcategoriaService(IMapper mapper, ISubcategoriaRepository subcategoriaRepository, 
            ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public Subcategoria CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDTO)
        {
            Categoria categoria = _categoriaRepository.GetById(subcategoriaDTO.CategoriaId);

            if (categoria == null) return null;

            if (categoria.Status == true)
            {
                Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDTO);
                _subcategoriaRepository.CriarSubcategoria(subcategoria);
                return subcategoria;
            }
            return null;
        }

        public ReadSubcategoriaDto RecuperarSubcategoriaPorId(int id)
        {
            Subcategoria subcategoria = _subcategoriaRepository.GetById(id);

            if (subcategoria != null)
            {
                ReadSubcategoriaDto subcategoriaDto = _mapper.Map<ReadSubcategoriaDto>(subcategoria);
                return subcategoriaDto;
            }
            return null;
        }

        public List<ReadSubcategoriaDto> PesquisarSubcategorias(int paginaAtual, int itensPagina, 
            bool? status, string nome, string order)
        {
            if (nome == null && status == null)
            {
                if (order == "desc")
                {
                    var retornoDesc = _mapper.Map<List<ReadSubcategoriaDto>>(
                        _subcategoriaRepository.GetAll().
                        OrderByDescending(c => c.Nome).Skip((paginaAtual - 1) * itensPagina).
                        Take(itensPagina).ToList());
                    return retornoDesc;
                }
                var retorno = _mapper.Map<List<ReadSubcategoriaDto>>(_subcategoriaRepository.GetAll().OrderBy(c => c.Nome).Skip((paginaAtual - 1) * itensPagina).
                Take(itensPagina).ToList());
                return retorno;
            }

            if (nome != null && status != null)
            {
                if (nome.Length < 3 || nome.Length > 128 || Regex.IsMatch(nome, "[^a-zA-Z]")
                    || (status != true && status != false))
                {
                    return null;
                }
                if (order == "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadSubcategoriaDto>>(
                    _subcategoriaRepository.GetAll().
                    Where(c => c.Nome.ToLower().Contains(nome.ToLower())).
                    Where(c => c.Status == status).OrderByDescending(c => c.Nome).
                    Skip((paginaAtual - 1) * itensPagina).
                    Take(itensPagina).
                    ToList());
                    return retornoDtoDesc;
                }
                var retornoDto = _mapper.Map<List<ReadSubcategoriaDto>>(
                    _subcategoriaRepository.GetAll().
                    Where(c => c.Nome.ToLower().Contains(nome.ToLower())).
                    Where(c => c.Status == status).OrderBy(c => c.Nome).
                    Skip((paginaAtual - 1) * itensPagina).
                    Take(itensPagina).
                    ToList());
                return retornoDto;
            }
            if (status != null)
            {
                if (status != true && status != false)
                {
                    return null;
                }
                if (order == "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadSubcategoriaDto>>(
                    _subcategoriaRepository.GetAll().Where(c => c.Status == status).
                    OrderByDescending(c => c.Nome).
                    Skip((paginaAtual - 1) * itensPagina).
                    Take(itensPagina).
                    ToList());
                    return retornoDtoDesc;
                }
                var retornoDto = _mapper.Map<List<ReadSubcategoriaDto>>(
                _subcategoriaRepository.GetAll().Where(c => c.Status == status).
                OrderBy(c => c.Nome).
                Skip((paginaAtual - 1) * itensPagina).
                Take(itensPagina).
                ToList());
                return retornoDto;
            }
            if (nome != null)
            {
                if (nome.Length < 3 || nome.Length > 128 || Regex.IsMatch(nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                if (order != "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadSubcategoriaDto>>(
                    _subcategoriaRepository.GetAll().Where
                    (c => c.Nome.ToLower().
                    Contains(nome.ToLower())).
                    OrderByDescending(c => c.Nome).
                    Skip((paginaAtual - 1) * itensPagina).
                    Take(itensPagina).
                    ToList());
                    return retornoDtoDesc;

                }
                var retornoDto = _mapper.Map<List<ReadSubcategoriaDto>>(
                _subcategoriaRepository.GetAll().Where
                (c => c.Nome.ToLower().
                Contains(nome.ToLower())).OrderBy(c => c.Nome).
                Skip((paginaAtual - 1) * itensPagina).
                Take(itensPagina).
                ToList());
                return retornoDto;
            }
            return null;
        }

        public Result DeletarSubcategoria(int id)
        {
            var subcategoria = _subcategoriaRepository.GetById(id);
            if (subcategoria != null)
            {
                _subcategoriaRepository.RemoverSubcategoria(subcategoria);              
                return Result.Ok();
            }
            return Result.Fail("Subcategoria não localizada.");
        }

        public Result EditarStatusSubcategoria(int id)
        {
            var subcategoriaAtualizar = _subcategoriaRepository.GetById(id);

            if (subcategoriaAtualizar != null)
            {
                if (subcategoriaAtualizar.Status == true)
                {
                    subcategoriaAtualizar.Status = false;
                }
                else
                {
                    subcategoriaAtualizar.Status = true;
                }
                _subcategoriaRepository.EditarStatus(subcategoriaAtualizar);
                return Result.Ok();
            }
            return Result.Fail("Subcategoria não localizada!");
        }

        public Result EditarNomeSubcategoria(int id, UpdateSubcategoriaDto subcategoriaDto)
        {
            var subcategoriaAtualizar = _subcategoriaRepository.GetById(id);

            if (subcategoriaAtualizar != null)
            {
                _mapper.Map(subcategoriaDto, subcategoriaAtualizar);
                _subcategoriaRepository.EditarSubcategoria(subcategoriaAtualizar);
                return Result.Ok();
            }
            return Result.Fail("Subcategoria não encontrada");
        }
    }
}
