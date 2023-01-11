# Ecommerce API
API Rest para um ecommerce.

# Tecnologias

As seguintes ferramentas foram usadas na construção do projeto:

- [Entity Framework](https://docs.microsoft.com/pt-br/ef/core/what-is-new/ef-core-5.0/whatsnew) - ORM
- [MySql](https://www.mysql.com/) - Banco de dados
- [AutoMapper](https://automapper.org/) 
- [Dapper](https://github.com/DapperLib/Dapper) - Consultas avançadas
- [Swagger](https://swagger.io/) - Documentação

# Abrir e rodar o projeto
Após clonar o repositório, abra-o com o Visual Studio, clicando duas vezes no arquivo .sln do projeto.

Para rodar pela primeira vez, com o visual aberto, vá em ```Ferramentas``` > ```Gerenciador de pacotes do NuGet``` > ```Console do Gerenciador de Pacotes```

Com o console aberto, em "Projeto Padrão" selecione o ```EcommerceAPI.Infra``` e escreva o seguinte comando:

```
Add-Migration Primeira-Migracao
```

Após confirmar, escreva:

```
Update-Database
```

Feita a migração para o banco, basta rodar o projeto. Certifique-se de que está selecionado o "EcommerceAPI".
