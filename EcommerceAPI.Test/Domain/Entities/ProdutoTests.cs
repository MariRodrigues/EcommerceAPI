using EcommerceAPI.Test.Domain.Entities.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceAPI.Test.Domain.Entities
{
    public class ProdutoTests
    {
        [Fact(DisplayName = "Deve ser possível criar um produto com status ativo")]
        public void Deve_Ser_Criado_Produto()
        {
            // Act
            var produto = ProdutoFactory.Create();

            // Assert
            Assert.NotNull(produto);
            Assert.True(produto.Status);
        }
    }
}
