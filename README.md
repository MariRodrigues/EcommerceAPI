# Ecommerce API
API Rest para um ecommerce, construído para fins de aprendizado.

## Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [Entity Framework](https://docs.microsoft.com/pt-br/ef/core/what-is-new/ef-core-5.0/whatsnew) - ORM
- [MySql](https://www.mysql.com/) - Banco de dados
- [AutoMapper](https://automapper.org/) 
- [Dapper](https://github.com/DapperLib/Dapper) - Micro ORM
- [Swagger](https://swagger.io/) - Documentação
- [XUnit]() - Ferramenta de testes de unidade
- [Bogus](https://github.com/bchavez/Bogus) - Framework para geração de dados Fake para auxiliar nos testes unitários

## Abrir e rodar o projeto
Após clonar o repositório, abra-o com o Visual Studio, clicando duas vezes no arquivo .sln do projeto.

É necessário acrescentar no arquivo ```appsettings.json``` as informações do banco de dados para realizar a conexão.
O arquivo ficará da seguinte forma:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "CategoriaConnection": "server=seu-servidor-ou-localhost;database=ecommerceDB;user=seu-usuario;password=sua-senha"
  }
}
```

Para rodar pela primeira vez, com o VS aberto, vá em ``Ferramentas`` > `Gerenciador de pacotes do NuGet` > ```Console do Gerenciador de Pacotes```

Com o console aberto, em "Projeto Padrão" selecione o ```EcommerceAPI.Infra``` e escreva o seguinte comando:

```
Add-Migration Primeira-Migracao
```

Após confirmar, escreva:

```
Update-Database
```

Feita a migração para o banco, basta rodar o projeto. Certifique-se de que está selecionado o "EcommerceAPI".
