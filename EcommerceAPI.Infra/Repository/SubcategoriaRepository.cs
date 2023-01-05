using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Infra.Data;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Repository
{
    public class SubcategoriaRepository
    {
        private readonly AppDbContext _context;

        public SubcategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Subcategoria CriarSubcategoria(Subcategoria subcategoria)
        {
            _context.Subcategorias.Add(subcategoria);
            _context.SaveChanges();
            return subcategoria;
        }

        public Subcategoria GetById(int id)
        {
            return _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
        }

        public IEnumerable<Subcategoria> GetAll()
        {
            return _context.Subcategorias.ToList();
        }

        public Result EditarSubcategoria(Subcategoria subcategoria)
        {
            subcategoria.DataModificacao = DateTime.Now;
            _context.Update(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarStatus(Subcategoria subcategoria)
        {
            subcategoria.DataModificacao = DateTime.Now;
            _context.Update(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public void RemoverSubcategoria(Subcategoria subcategoria)
        {
            _context.Subcategorias.Remove(subcategoria);
            _context.SaveChanges();
        }
    }
}
