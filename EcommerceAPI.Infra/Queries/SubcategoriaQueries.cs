using Dapper;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Infra.Queries
{
    public class SubcategoriaQueries : ISubcategoriaQueries
    {
        private readonly MySqlConnection _connection;

        public SubcategoriaQueries(AppDbContext context)
        {
            _connection = new MySqlConnection(context.Database.GetConnectionString());
        }

        public async Task<IEnumerable<ReadSubcategoriaDto>> RecuperarSubcategoriaFilter(SubcategoriaFiltros filtros)
        {
            var queryArgs = new DynamicParameters();

            queryArgs.Add("Pag", filtros.Pagina);
            queryArgs.Add("ItensPagina", filtros.ItensPagina);

            var query = @"SELECT 
                            s.Id as SubcategoriaId,
                            s.Nome,
                            s.Status,
                            s.DataCriacao,
                            s.DataModificacao,
                            c.Id as Categorias_CategoriaId,
                            c.Nome as Categorias_Nome,
                            p.Id as Produtos_ProdutoId,
                            p.Nome as Produtos_Nome

                            FROM subcategorias s
                            LEFT JOIN categorias c ON c.Id = s.categoriaId
                            LEFT JOIN produtos p ON p.SubcategoriaId = s.Id 
                            WHERE ";

            if (filtros.Id != null)
            {
                query += " s.Id = @Id AND";
                queryArgs.Add("Id", filtros.Id);
            }

            if (filtros.Nome != null)
            {
                query += " s.Nome LIKE CONCAT('%',@Nome,'%') AND";
                queryArgs.Add("Nome", filtros.Nome);
            }

            if (filtros.Status != null)
            {
                query += " s.Status = @Status AND";
                queryArgs.Add("Status", filtros.Status);
            }

            if (filtros.Nome == null && filtros.Status == null && filtros.Id == null)
                query = query.Remove(query.LastIndexOf("WHERE"));
            else
                query = query.Remove(query.LastIndexOf("AND"));

            if (filtros.Ordem == "desc")
            {
                query += " ORDER BY s.nome DESC ";
            }
            else
            {
                query += " ORDER BY s.nome ";
            }

            var offset = (filtros.Pagina - 1) * filtros.ItensPagina;

            query += " LIMIT @ItensPagina OFFSET @offset";

            queryArgs.Add("offset", offset);

            var result = await _connection.QueryAsync(query, queryArgs);

            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(ReadSubcategoriaDto), "SubcategoriaId");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(Categorias), "CategoriaId");
            Slapper.AutoMapper.Configuration.AddIdentifier(typeof(Produtos), "ProdutoId");

            var resultFinal = Slapper.AutoMapper.MapDynamic<ReadSubcategoriaDto>(result);
            return resultFinal;
        }

        public async Task<Subcategoria> RecuperarSubcategoriaPorId(int id)
        {
            var query = @"SELECT * FROM subcategorias 
                            WHERE Id = @Id";

            var result =  _connection.Query<Subcategoria>(query, new { id }).FirstOrDefault();
            return result;
        }
    }
}
