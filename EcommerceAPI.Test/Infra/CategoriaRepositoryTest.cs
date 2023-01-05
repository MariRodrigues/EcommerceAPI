using EcommerceAPI.Infra.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceAPI.Test.Infra
{
    public class CategoriaRepositoryTest
    {
        private readonly CategoriaRepository _repository;
        public CategoriaRepositoryTest()
        {
            var service = new ServiceCollection();
            service.AddScoped<CategoriaRepository, CategoriaRepository>();
            var provider = service.BuildServiceProvider();
            _repository = provider.GetService<CategoriaRepository>();
        }

        [Fact]
        public void TestGetAllCaetegories()
        {
            var list = _repository.GetAll();

            Assert.NotNull(list);
            Assert.Equal(28, list.Count());
        }
    }
}
