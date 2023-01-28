using EcommerceAPI.Test.Domain.Entities.Factory;
using Xunit;

namespace EcommerceAPI.Test.Domain.Entities
{
    public class SubcategoriaTests
    {
        [Fact(DisplayName = "Deve ser possível criar uma subcategoria")]
        public void Deve_Ser_Criada_Subcategoria()
        {
            // Act
            var subcategoria = SubcategoriaFactory.Create(2);

            // Assert
            Assert.NotNull(subcategoria);
            Assert.True(subcategoria.Status);
            Assert.Equal(2, subcategoria.Produtos.Count);
        }
    }
}
