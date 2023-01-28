using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Domain.Produtos;
using System.Collections.Generic;

namespace EcommerceAPI.Domain.Queries
{
    public interface IProdutoQueries
    {
        List<Produto> GetAllProducts();
        Produto GetById(int id);
        IEnumerable<Produto> GetAllFilter(FiltrosProduto filtros);
    }
}
