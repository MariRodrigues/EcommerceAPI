using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Repository 
{
    public class CategoriaRepository : ICategoriaRepository
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

        public Categoria EditarCategoria(Categoria categoriaAtualizar)
        {
            categoriaAtualizar.DataModificacao = DateTime.Now;
            _context.Categorias.Update(categoriaAtualizar);
            _context.SaveChanges();
            return categoriaAtualizar;
        }

        public void DeletarCategoria(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
        }
    }
}
