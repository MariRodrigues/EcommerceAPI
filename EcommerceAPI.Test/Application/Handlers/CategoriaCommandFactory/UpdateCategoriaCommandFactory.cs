using Bogus;
using EcommerceAPI.Application.Commands.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Application.Handlers.CategoriaCommandFactory
{
    public static class UpdateCategoriaCommandFactory
    {
        public static UpdateCategoriaCommand UpdateCategoriaCommand(int id, string nome)
        {
            var categoriaCommand = new UpdateCategoriaCommand();
            categoriaCommand.Nome = nome; 
            categoriaCommand.Id= id;
            
            return categoriaCommand;
        }

        public static UpdateCategoriaCommand UpdateCategoriaCommand()
        {
            var categoriaCommand = new Faker<UpdateCategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName())
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return categoriaCommand;
        }
    }
}
