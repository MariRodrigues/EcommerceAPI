using Bogus;
using EcommerceAPI.Application.Commands.Subcategorias;
namespace EcommerceAPI.Test.Application.Handlers.SubcategoriaCommandFactory
{
    public static class UpdateSubcategoriaCommandFactory
    {
        public static UpdateSubcategoriaCommand Create()
        {
            var subcategoriaCommand = new Faker<UpdateSubcategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return subcategoriaCommand;
        }

        public static UpdateSubcategoriaCommand Create(string nome)
        {
            var subcategoriaCommand = new Faker<UpdateSubcategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => nome)
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return subcategoriaCommand;
        }
    }
}
