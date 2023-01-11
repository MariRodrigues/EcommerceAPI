using AutoMapper;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Categorias.DTO;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Infra.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Application.Services
{
    public class CategoriaService
    {
        private readonly IMapper _mapper;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly CategoriaQueries _categoriaQueries;

        public CategoriaService(IMapper mapper, ICategoriaRepository categoriaRepository,
            IProdutoRepository produtoRepository, ISubcategoriaRepository subcategoriaRepository, CategoriaQueries categoriaQueries)
        {
            _mapper = mapper;
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaQueries = categoriaQueries;
        }
        public Categoria CadastrarCategoria(CreateCategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _categoriaRepository.CadastrarCategoria(categoria);

            return categoria;
        }
        public ReadCategoriaDto RecuperaCategoriaPorId(int id)
        {
            FiltrosCategoria filtros = new FiltrosCategoria()
            {
                Id = id
            };

            var categoriaDto = _categoriaQueries.GetAllFilter(filtros).Result.FirstOrDefault();
            return categoriaDto;
        }

        public List<ReadCategoriaDto> PesquisarCategorias(FiltrosCategoria filtros)
        {
            if (filtros.Nome == null && filtros.Status == null && filtros.Ordem == null)
            {
                var categorias = _categoriaRepository
                    .GetAll()
                    .OrderBy(c => c.Nome)
                    .Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina).ToList();
                var retorno = _mapper.Map<List<ReadCategoriaDto>>(categorias
                    );
                return retorno;
            }

            if (filtros.Nome != null && filtros.Status != null)
            {
                if (ValidarNome(filtros.Nome) == false)
                {
                    return null;
                }

                if (filtros.Ordem == "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadCategoriaDto>>(
                    _categoriaRepository.GetAll().
                    Where(c => c.Nome.ToLower().Contains(filtros.Nome.ToLower())).
                    Where(c => c.Status == filtros.Status).OrderByDescending(c => c.Nome).
                    Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina).
                    ToList());
                    return retornoDtoDesc;
                }
                var retornoDto = _mapper.Map<List<ReadCategoriaDto>>(
                    _categoriaRepository.GetAll().
                    Where(c => c.Nome.ToLower().Contains(filtros.Nome.ToLower())).
                    Where(c => c.Status == filtros.Status).OrderBy(c => c.Nome).
                    Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina).
                    ToList());
                return retornoDto;
            }
            if (filtros.Status != null)
            {
                if (filtros.Status != true && filtros.Status != false)
                {
                    return null;
                }
                if (filtros.Ordem == "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadCategoriaDto>>(
                    _categoriaRepository.GetAll().Where(c => c.Status == filtros.Status).
                    OrderByDescending(c => c.Nome).
                    Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina).
                    ToList());
                    return retornoDtoDesc;
                }
                var retornoDto = _mapper.Map<List<ReadCategoriaDto>>(
                _categoriaRepository.GetAll().Where(c => c.Status == filtros.Status).
                OrderBy(c => c.Nome).
                Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                Take(filtros.ItensPagina).
                ToList());
                return retornoDto;
            }
            if (filtros.Nome != null)
            {
                if (ValidarNome(filtros.Nome) == false)
                {
                    return null;
                }
                if (filtros.Ordem != "desc")
                {
                    var retornoDtoDesc = _mapper.Map<List<ReadCategoriaDto>>(
                    _categoriaRepository.GetAll().Where
                    (c => c.Nome.ToLower().
                    Contains(filtros.Nome.ToLower())).
                    OrderByDescending(c => c.Nome).
                    Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina).
                    ToList());
                    return retornoDtoDesc;

                }
                var retornoDto = _mapper.Map<List<ReadCategoriaDto>>(
                _categoriaRepository.GetAll().Where
                (c => c.Nome.ToLower().
                Contains(filtros.Nome.ToLower())).OrderBy(c => c.Nome).
                Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                Take(filtros.ItensPagina).
                ToList());
                return retornoDto;
            }
            return null;
        }
        public ReadCategoriaDto EditarStatusCategoria(int id)
        {
            var categoriaAtualizar = _categoriaRepository.GetById(id);

            var produtos = _produtoRepository.GetAll().Where(p => p.CategoriaId == categoriaAtualizar.Id).ToList();

            if (categoriaAtualizar != null)
            {
                if (categoriaAtualizar.Status == true)
                {
                    if (produtos.Count != 0)
                    {
                        return null;
                    }
                    else
                    {
                        categoriaAtualizar.Status = false;
                        categoriaAtualizar.DataModificacao = DateTime.Now;

                        var listaSubcategorias = _subcategoriaRepository.GetAll().Where(s => s.CategoriaId == id).ToList();

                        foreach (Subcategoria sub in listaSubcategorias)
                        {
                            sub.Status = false;
                        }
                    }
                }
                else
                {
                    categoriaAtualizar.Status = true;
                    categoriaAtualizar.DataModificacao = DateTime.Now;

                    var listaSubcategorias = _subcategoriaRepository.GetAll().Where(s => s.CategoriaId == id).ToList();

                    foreach (Subcategoria sub in listaSubcategorias)
                    {
                        sub.Status = true;
                    }
                }
                _categoriaRepository.EditarCategoria(categoriaAtualizar);
                
                ReadCategoriaDto categoriaDto = _mapper.Map<ReadCategoriaDto>(categoriaAtualizar);
                return categoriaDto;
            }
            return null;
        }

        public Categoria EditarCategoria(int id, UpdateCategoriaDto categoriaDto)
        {
            var categoriaAtualizar = _categoriaRepository.GetById(id);

            if (categoriaAtualizar != null)
            {
                _mapper.Map(categoriaDto, categoriaAtualizar);
                _categoriaRepository.EditarCategoria(categoriaAtualizar);   
                return categoriaAtualizar;
            }
            return null;
        }

        private static bool ValidarNome(string nome)
        {
            if (nome.Length < 3 || nome.Length > 128 || Regex.IsMatch(nome, "[^a-zA-Z]"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
