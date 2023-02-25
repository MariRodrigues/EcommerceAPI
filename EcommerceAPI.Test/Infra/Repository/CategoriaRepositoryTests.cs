using EcommerceAPI.Test.Domain.Entities.Factory;
using EcommerceAPI.Test.Infra.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceAPI.Test.Infra.Repository
{
    public class CategoriaRepositoryTests
    {
        [Fact(DisplayName = "Deve ser possível se comunicar com o banco de dados")]
        public void Testa_Cadastro_Categoria()
        {
            // Arrange
            var categoria = CategoriaFactory.Create();
            DbCategoriaFactory.Create(categoria);

            var categoryAdded = DbCategoriaFactory.GetById(categoria.Id);

            Assert.NotNull(categoryAdded);
        }
    }
}
