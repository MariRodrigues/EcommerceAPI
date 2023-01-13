using Bogus;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Produtos;
using System.Collections.Generic;

namespace EcommerceAPI.Test.Domain.Entities.Factory
{
    public static class ProdutoFactory
    {
        public static List<Produto> Create(int qty)
        {
            var produto = new Faker<Produto>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.LastName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker)
                .RuleFor(x => x.Peso, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Altura, f => f.Random.Number(1, 10))
                .RuleFor(x => x.QuantidadeEstoque, f => f.Random.Number(1, 100))
                .RuleFor(x => x.CentroDistribuicaoId, f => f.IndexFaker)
                .RuleFor(x => x.SubcategoriaId, f => f.IndexFaker)
                .RuleFor(x => x.CategoriaId, f => f.IndexFaker)
                .RuleFor(x => x.Comprimento, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Url, f => f.Internet.Avatar())
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Descricao, f => f.Lorem.Sentence());

            var produtos = produto.Generate(qty);

            return produtos;
        }

        public static Produto Create()
        {
            var produto = new Faker<Produto>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.LastName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker)
                .RuleFor(x => x.Peso, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Altura, f => f.Random.Number(1, 10))
                .RuleFor(x => x.QuantidadeEstoque, f => f.Random.Number(1, 100))
                .RuleFor(x => x.CentroDistribuicaoId, f => f.IndexFaker)
                .RuleFor(x => x.SubcategoriaId, f => f.IndexFaker)
                .RuleFor(x => x.CategoriaId, f => f.IndexFaker)
                .RuleFor(x => x.Comprimento, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Url, f => f.Internet.Avatar())
                .RuleFor(x => x.Valor, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Descricao, f => f.Lorem.Sentence());

            return produto;
        }

    }
}
