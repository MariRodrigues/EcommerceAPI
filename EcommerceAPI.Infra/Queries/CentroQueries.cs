using Dapper;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace EcommerceAPI.Infra.Queries
{
    public class CentroQueries
    {
        private readonly MySqlConnection _connection;

        public CentroQueries(AppDbContext context)
        {
            _connection = new MySqlConnection(context.Database.GetConnectionString());
        }

        public IEnumerable<CentroDistribuicao> GetAllFilter(FiltrosCD filtros)
        {
            var queryArgs = new DynamicParameters();

            var sql = "SELECT * FROM centrosdistribuicao WHERE ";

            if (filtros.Id != null)
            {
                sql += "Id = @id and ";
                queryArgs.Add("id", filtros.Id);
            }
            if (filtros.Bairro != null)
            {
                sql += "bairro like CONCAT('%',@bairro,'%') and ";
                queryArgs.Add("bairro", filtros.Bairro);
            }
            if (filtros.Status != null)
            {
                sql += "status = @status and ";
                queryArgs.Add("status", filtros.Status);
            }
            if (filtros.Numero != null)
            {
                sql += "numero = @numero and ";
                queryArgs.Add("numero", filtros.Numero);
            }
            if (filtros.CEP != null)
            {
                sql += "CEP = @cep and ";
                queryArgs.Add("cep", filtros.CEP);
            }
            if (filtros.UF != null)
            {
                sql += "UF = @uf and ";
                queryArgs.Add("uf", filtros.UF);
            }
            if (filtros.Cidade != null)
            {
                sql += "cidade like CONCAT('%',@cidade,'%') and ";
                queryArgs.Add("cidade", filtros.Cidade);
            }
            if (filtros.Nome != null)
            {
                sql += "nome like CONCAT('%',@nome,'%') and ";
                queryArgs.Add("nome", filtros.Nome);
            }
            if (filtros.Complemento != null)
            {
                sql += "complemento like CONCAT('%',@complemento,'%') and ";
                queryArgs.Add("complemento", filtros.Complemento);
            }
            if (filtros.Logradouro != null)
            {
                sql += "logradouro like CONCAT('%',@logradouro,'%') and ";
                queryArgs.Add("logradouro", filtros.Logradouro);
            }

            if (filtros.Id == null && filtros.Status == null && filtros.Bairro == null
                && filtros.CEP == null && filtros.Numero == null
                && filtros.Logradouro == null && filtros.Complemento == null
                && filtros.Nome == null && filtros.Cidade == null)
            {
                sql = sql.Remove(sql.LastIndexOf("WHERE"));
            }
            else
            {
                var posicaoAnd = sql.LastIndexOf("and");
                sql = sql.Remove(posicaoAnd);
            }
            if (filtros.Ordem == "desc")
            {
                sql += "order by nome desc ";
            }
            else
            {
                sql += "order by nome ";
            }

            var offset = (filtros.Pagina - 1) * filtros.ItensPagina;

            sql += "LIMIT @ItensPagina OFFSET @offset";

            queryArgs.Add("ItensPagina", filtros.ItensPagina);
            queryArgs.Add("offset", offset);

            var result = _connection.Query<CentroDistribuicao>(sql, queryArgs);

            return result;
        }
    }
}
