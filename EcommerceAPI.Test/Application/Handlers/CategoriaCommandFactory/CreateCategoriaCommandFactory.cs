using Bogus;
using EcommerceAPI.Application.Commands.Categorias;

namespace EcommerceAPI.Test.Application.Handlers.CategoriaCommandFactory
{
    public static class CreateCategoriaCommandFactory
    {
        public static CreateCategoriaCommand Create(string nome)
        {
            var categoriaCommand = new CreateCategoriaCommand();
            categoriaCommand.Nome = nome;

            return categoriaCommand;
        }

        public static CreateCategoriaCommand Create()
        {
            var categoriaCommand = new Faker<CreateCategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName());

            return categoriaCommand;
        }
    }
}
