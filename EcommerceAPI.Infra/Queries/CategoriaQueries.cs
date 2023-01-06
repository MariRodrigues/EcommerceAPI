using Dapper;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Categorias.DTO;
using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infra.Queries
{
    public class CategoriaQueries
    {
        private readonly MySqlConnection _connection;

        public CategoriaQueries(AppDbContext context)
        {
            _connection = new MySqlConnection(context.Database.GetConnectionString());
        }

        public async Task<IEnumerable<ReadCategoriaDto>> GetAllFilter(FiltrosCategoria filtros)
        {
            var queryArgs = new DynamicParameters();

            var query = @"SELECT 
                            c.Id as CategoriaId,
                            c.Nome,
                            c.Status,
                            c.DataCriacao,
                            s.Id as Subcategorias_SubcategoriaId,
                            s.Nome as Subcategorias_Nome,
                            p.Id as Produtos_ProdutoId,
                            p.Nome as Produtos_Nome

                            FROM CATEGORIAS c
                            LEFT JOIN subcategorias s ON s.CategoriaId = c.Id
                            LEFT JOIN produtos p ON p.CategoriaId = c.Id
                            WHERE ";

            if (filtros.Id != null)
            {
                query += " c.Id = @Id AND";
                queryArgs.Add("Id", filtros.Id);
            }

            if (filtros.Nome != null)
            {
                query += " c.Nome LIKE CONCAT('%',@Nome,'%') AND";
                queryArgs.Add("Nome", filtros.Nome);
            }

            if (filtros.Status != null)
            {
                query += " c.Status = @Status AND";
                queryArgs.Add("Status", filtros.Status);
            }

            if (filtros.Id == null && filtros.Nome == null && filtros.Status == null)
                query = query.Remove(query.LastIndexOf("WHERE"));
            else
                query = query.Remove(query.LastIndexOf("AND"));

            if (filtros.Ordem == "desc")
            {
                query += " ORDER BY c.nome DESC ";
            }
            else
            {
                query += " ORDER BY c.nome ";
            }

            var result = await _connection.QueryAsync(query, queryArgs);

            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(ReadCategoriaDto), "CategoriaId");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(Subcategorias), "SubcategoriaId");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(Produtos), "ProdutoId");

            var resultFinal = Slapper.AutoMapper.MapDynamic<ReadCategoriaDto>(result).Skip((filtros.Pagina - 1) * filtros.ItensPagina).
                    Take(filtros.ItensPagina);
            return resultFinal;
        }
    }
}
