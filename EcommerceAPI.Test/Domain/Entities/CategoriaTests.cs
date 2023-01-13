using EcommerceAPI.Test.Domain.Entities.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
