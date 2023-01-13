using Bogus;
using EcommerceAPI.Application.Commands.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Application.Handlers.CategoriaCommandFactory
{
    public static class UpdateStatusCategoriaCommandFactory
    {
        public static UpdateStatusCategoriaCommand UpdateStatusCategoriaCommand()
        {
            var categoriaCommand = new Faker<UpdateStatusCategoriaCommand>("pt_BR")
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return categoriaCommand;
        }

        public static UpdateStatusCategoriaCommand UpdateStatusCategoriaCommand(int id)
        {
            var categoriaCommand = new UpdateStatusCategoriaCommand();
            categoriaCommand.Id = id;

            return categoriaCommand;
        }
    }
}
