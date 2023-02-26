using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Test.Domain.Entities.Factory;
using EcommerceAPI.Test.Infra.Shared;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EcommerceAPI.Test.Infra.Repository
{
    public class CategoriaRepositoryTests
    {
        [Fact(DisplayName = "Deve ser possível criar uma categoria no banco")]
        public void Cadastra_Categoria_E_Busca_Por_ID()
        {
            // Arrange
            var categoria = CategoriaFactory.Create();
            DbCategoriaFactory.Create(categoria);

            // Act
            var categoryAdded = DbCategoriaFactory.GetById(categoria.Id);

            // Assert
            Assert.NotNull(categoryAdded);
            Assert.Null(categoryAdded.DataModificacao);

            DbCategoriaFactory.Delete(categoryAdded);
        }

        [Fact(DisplayName = "Deve ser possível criar uma categoria e atualiza-la no banco")]
        public void Cadastra_Categoria_E_Atualiza()
        {
            // Arrange
            var categoria = CategoriaFactory.Create();

            // Act
            DbCategoriaFactory.Create(categoria);

            string name = "updated_" + categoria.Nome;
            categoria.Nome = name;
            var categoriaUpdated = DbCategoriaFactory.Edit(categoria);

            // Assert
            Assert.Equal(name, categoriaUpdated.Nome);
            Assert.NotNull(categoriaUpdated.DataModificacao);

            DbCategoriaFactory.Delete(categoria);
        }

        [Fact(DisplayName = "Deve ser possível criar mais de uma categoria e retorna-las")]
        public void Cadastra_Categorias_Busca_Todas()
        {
            // Arrange
            var categoria1 = CategoriaFactory.Create();
            var categoria2 = CategoriaFactory.Create();

            List<Categoria> listCategorias = new List<Categoria>
            {
                categoria1,
                categoria2
            };

            // Act
            foreach(var item in listCategorias)
            {
                DbCategoriaFactory.Create(item);
            }

            var retorno = DbCategoriaFactory.BuscarTodos();

            // Assert
            Assert.True(retorno.Count() > 1);

            // Deletar as categorias
            foreach (var item in listCategorias)
            {
                DbCategoriaFactory.Delete(item);
            }
        }
    }
}
