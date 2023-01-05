using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Infra.Data;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Repository
{
    public class CategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Categoria GetById(int id)
        {
            return _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        }

        public Categoria CadastrarCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public IEnumerable<Categoria> GetAll()
        {
            return _context.Categorias.ToList();
        }

        public Result EditarNome(Categoria categoriaAtualizar)
        {
            categoriaAtualizar.DataModificacao = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarStatus(Categoria categoriaAtualizar)
        {
            categoriaAtualizar.DataModificacao = DateTime.Now;
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
