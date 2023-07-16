using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Infra.Data;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using System.Collections;
using System.Collections.Generic;

namespace EcommerceAPI.Test.Infra.Shared
{
    public static class DbCategoriaFactory
    {
        public static CategoriaRepository CreateCategoriaRepository(AppDbContext context)
        {
            return new CategoriaRepository(context);
        }

        public static CategoriaQueries CreateCategoriaQueries(AppDbContext context)
        {
            return new CategoriaQueries(context);
        }

        public static void Create(Categoria categoria)
        {
            var contextRepository = CreateCategoriaRepository(DbFactory.CreateAppDbContext());
            contextRepository.CadastrarCategoria(categoria);
        }

        public static Categoria GetById(int id)
        {
            var contextRepository = CreateCategoriaRepository(DbFactory.CreateAppDbContext());
            return contextRepository.GetById(id);
        }

        public static Categoria Edit(Categoria categoria)
        {
            var contextRepository = CreateCategoriaRepository(DbFactory.CreateAppDbContext());
            return contextRepository.EditarCategoria(categoria);
        }

        public static void Delete(Categoria categoria)
        {
            var contextRepository = CreateCategoriaRepository(DbFactory.CreateAppDbContext());
            contextRepository.DeletarCategoria(categoria);
        }

        public static IEnumerable<Categoria> BuscarTodos()
        {
            var contextRepository = CreateCategoriaRepository(DbFactory.CreateAppDbContext());
            return contextRepository.GetAll();
        }
    }
}
