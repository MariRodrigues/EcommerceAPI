using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Infra.Data;
using EcommerceAPI.Infra.Repository;

namespace EcommerceAPI.Test.Infra.Shared
{
    public static class DbCategoriaFactory
    {
        public static CategoriaRepository CreateCategoriaRepository(AppDbContext context)
        {
            return new CategoriaRepository(context);
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
    }
}
