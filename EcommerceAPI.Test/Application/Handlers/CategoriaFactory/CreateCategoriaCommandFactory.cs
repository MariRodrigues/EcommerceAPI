﻿using Bogus;
using EcommerceAPI.Application.Commands.Categorias;

namespace EcommerceAPI.Test.Application.Handlers.CategoriaFactory
{
    public static class CreateCategoriaCommandFactory
    {
        public static CreateCategoriaCommand CreateCategoriaCommand(string nome)
        {
            var categoriaCommand = new CreateCategoriaCommand();
            categoriaCommand.Nome = nome;

            return categoriaCommand;
        }

        public static CreateCategoriaCommand CreateCategoriaCommand()
        {
            var categoriaCommand = new Faker<CreateCategoriaCommand>("pt_BR")
                .RuleFor(x => x.Nome, (f, u) => f.Name.FullName());

            return categoriaCommand;
        }
    }
}