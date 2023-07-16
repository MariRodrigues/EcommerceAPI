using EcommerceAPI.Domain.Categorias.DTO;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Test.Domain.Entities.Factory;
using EcommerceAPI.Test.Infra.Shared;
using Xunit;

namespace EcommerceAPI.Test.Infra.Queries
{
    public class CategoriaQueriesTests
    {
        [Fact(DisplayName ="Deve ser possível buscar categorias por filtros")]
        public void Deve_Retornar_Categorias_Por_Filtros()
        {
            // Arrange
            var categoria = CategoriaFactory.Create("Andre");
            DbCategoriaFactory.Create(categoria);
            FiltrosCategoria filtros = new()
            {
                Nome = "And"
            };

            // Act
            var result = new CategoriaQueries(DbFactory.CreateAppDbContext()).GetAllFilter(filtros);

            // Assert
            Assert.NotNull(result);

            DbCategoriaFactory.Delete(categoria);
        }

        [Fact(DisplayName = "Deve ser possível retornar uma categoria com ID inexistente")]
        public async void Nao_Deve_Retornar_Categorias_Por_Filtros()
        {
            // Arrange
            
            FiltrosCategoria filtros = new()
            {
                Id = 99999999
            };

            // Act
            var result = new CategoriaQueries(DbFactory.CreateAppDbContext()).GetAllFilter(filtros);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(await result);
        }


    }
}
