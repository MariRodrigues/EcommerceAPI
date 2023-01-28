using Bogus;
using EcommerceAPI.Application.Commands.Subcategorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Test.Application.Handlers.SubcategoriaCommandFactory
{
    public static class UpdateStatusSubcategoriaCommandFactory
    {
        public static UpdateStatusSubcategoriaCommand Create()
        {
            var subcategoriaCommand = new Faker<UpdateStatusSubcategoriaCommand>("pt_BR")
                .RuleFor(x => x.Id, (f, u) => f.IndexFaker);

            return subcategoriaCommand;
        }
    }
}
