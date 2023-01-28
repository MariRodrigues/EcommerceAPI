using Dapper;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Queries
{
    public class ProdutoQueries : IProdutoQueries
    {
        private readonly MySqlConnection _connection;

        public ProdutoQueries(AppDbContext context)
        {
            _connection = new MySqlConnection(context.Database.GetConnectionString());
        }

        public List<Produto> GetAllProducts()
        {
            var query = @"SELECT * FROM produtos";

            var result =  _connection.Query<Produto>(query).ToList();
            return result;
        }

        public Produto GetById(int id)
        {
            var sql = "Select * from produtos where Id = @Id";

            var result = _connection.QuerySingleOrDefault<Produto>(sql, new { id });

            return result;
        }

        public IEnumerable<Produto> GetAllFilter(FiltrosProduto filtros)
        {
            var queryArgs = new DynamicParameters();

            var sql = "SELECT p.id, p.nome, p.descricao, p.status, p.datacriacao, " +
                "p.datamodificacao, p.peso, p.altura, p.largura, p.comprimento, p.valor, " +
                "p.quantidadeestoque, p.categoriaid, p.subcategoriaid, p.CentroDistribuicaoId, p.Url, " +
                "c.nome as Categoria, d.Nome as centroDistribuicao " +
                "FROM produtos p " +
                "INNER JOIN categorias c ON p.CategoriaId = c.Id " +
                "inner join centrosdistribuicao d ON d.Id = p.CentroDistribuicaoId " +
                "WHERE ";

            if (filtros.Peso != null)
            {
                sql += "p.peso = @peso and ";
                queryArgs.Add("peso", filtros.Peso);
            }

            if (filtros.Altura != null)
            {
                sql += "p.altura = @altura and ";
                queryArgs.Add("altura", filtros.Altura);
            }
            if (filtros.Nome != null)
            {
                sql += "p.nome LIKE CONCAT('%',@nome,'%') and ";
                queryArgs.Add("nome", filtros.Nome);
            }
            if (filtros.Status != null)
            {
                sql += "p.status = @status and ";
                queryArgs.Add("status", filtros.Status);
            }
            if (filtros.Largura != null)
            {
                sql += "p.largura = @largura and ";
                queryArgs.Add("largura", filtros.Largura);
            }
            if (filtros.Comprimento != null)
            {
                sql += "p.comprimento = @comprimento and ";
                queryArgs.Add("comprimento", filtros.Comprimento);
            }
            if (filtros.Valor != null)
            {
                sql += "p.valor = @valor and ";
                queryArgs.Add("valor", filtros.Valor);
            }
            if (filtros.QuantidadeEstoque != null)
            {
                sql += "p.quantidadeEstoque = @quantidadeEstoque and ";
                queryArgs.Add("quantidadeEstoque", filtros.QuantidadeEstoque);
            }
            if (filtros.CentroDistribuicao != null)
            {
                sql += "d.nome LIKE CONCAT('%',@centroDistribuicao,'%') and";
                queryArgs.Add("centroDistribuicao", filtros.CentroDistribuicao);
            }

            if (filtros.Peso == null && filtros.Nome == null && filtros.Altura == null && filtros.Status == null
                && filtros.Largura == null && filtros.Comprimento == null && filtros.Valor == null
                && filtros.QuantidadeEstoque == null && filtros.CentroDistribuicao == null)
            {
                var posicaoWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(posicaoWhere);
            }
            else
            {
                var posicaoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoAnd);
            }

            if (filtros.TipoOrdem == "desc")
            {
                if (filtros.OrdenarPor == "categoria")
                {
                    sql += "order by c.nome desc";
                }
                else if (filtros.OrdenarPor == "centro")
                {
                    sql += "order by p.centroDistribuicao desc";
                }
                else sql += "order by p.nome desc";
            }
            else
            {
                if (filtros.OrdenarPor == "categoria")
                {
                    sql += "order by c.nome";
                }
                else if (filtros.OrdenarPor == "centro")
                {
                    sql += "order by p.centroDistribuicao";
                }
                else
                {
                    sql += "order by p.nome";
                }
            }

            var result = _connection.Query<Produto, Categoria, CentroDistribuicao, Produto>
                (sql, (produto, categoria, centro) => {
                    produto.CategoriaId = categoria.Id;
                    produto.CentroDistribuicaoId = centro.Id;
                    return produto;
                }, queryArgs, splitOn: "CategoriaId, CentroDistribuicaoId").Skip((filtros.Pagina - 1) * filtros.ItensPagina).Take(filtros.ItensPagina);

            return result;
        }
    }
}
