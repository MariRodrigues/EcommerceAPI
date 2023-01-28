using Bogus;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Subcategorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Domain.Entities.Factory
{
    public static class SubcategoriaFactory
    {
        public static Subcategoria Create(int products)
        {
            var produtos = ProdutoFactory.Create(products);
            var categoria = CategoriaFactory.Create();

            var subcategoria = new Faker<Subcategoria>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker)
                .RuleFor(x => x.Produtos, (f, u) => produtos)
                .RuleFor(x => x.Categoria, (f, u) => categoria)
                .RuleFor(x => x.CategoriaId, (f, u) => categoria.Id);

            return subcategoria;
        }

        public static Subcategoria CreateWithStatusFalse(int products)
        {
            var produtos = ProdutoFactory.Create(products);
            var categoria = CategoriaFactory.Create();

            var subcategoria = new Faker<Subcategoria>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker)
                .RuleFor(x => x.Status, (f, u) => false)
                .RuleFor(x => x.Produtos, (f, u) => produtos)
                .RuleFor(x => x.Categoria, (f, u) => categoria)
                .RuleFor(x => x.CategoriaId, (f, u) => categoria.Id);

            return subcategoria;
        }
    }
}
