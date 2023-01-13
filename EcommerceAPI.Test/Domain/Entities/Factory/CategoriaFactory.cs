using Bogus;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Domain.Categorias;


namespace EcommerceAPI.Test.Domain.Entities.Factory
{
    public static class CategoriaFactory
    {
        public static Categoria Create()
        {
            var categoria = new Faker<Categoria>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return categoria;
        }

        public static Categoria Create(string nome)
        {
            var categoria = new Faker<Categoria>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => nome)
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return categoria;
        }
    }
}
