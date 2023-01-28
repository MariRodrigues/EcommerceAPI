using Bogus;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Commands.Subcategorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Application.Handlers.SubcategoriaCommandFactory
{
    public static class CreateSubcategoriaCommandFactory
    {
        public static CreateSubcategoriaCommand Create()
        {
            var subcategoriaCommand = new Faker<CreateSubcategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.CategoriaId, (f, u) => f.IndexFaker);

            return subcategoriaCommand;
        }

        public static CreateSubcategoriaCommand Create(string nome)
        {
            var subcategoriaCommand = new Faker<CreateSubcategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => nome)
                .RuleFor(x => x.CategoriaId, (f, u) => f.IndexFaker);

            return subcategoriaCommand;
        }
    }
}
