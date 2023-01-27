using EcommerceAPI.Test.Domain.Entities.Factory;
using Xunit;

namespace EcommerceAPI.Test.Domain.Entities
{
    public class CategoriaTests
    {
        [Fact(DisplayName ="Deve ser possível criar uma categoria")]
        public void Deve_Ser_Criada_Categoria()
        {
            // Act
            var categoria = CategoriaFactory.Create();

            // Assert
            Assert.NotNull(categoria);
        }

        [Fact(DisplayName = "Status da categoria criada deve ser ativa")]
        public void Deve_Ser_Criada_Categoria_Ativa()
        {
            // Act
            var categoria = CategoriaFactory.Create();

            // Assert
            Assert.True(categoria.Status);
        }

        [Fact(DisplayName = "Status da categoria criada deve ser ativa")]
        public void Deve_Ser_Criada_Categoria_Ativa_fail()
        {
            // Act
            var categoria = CategoriaFactory.Create();

            // Assert
            Assert.False(categoria.Status);
        }

    }
}
