using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Produtos.DTO;
using EcommerceAPI.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Repository
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> GetAll();
        Produto GetById(int id);
        IEnumerable<Produto> GetAllFilter(FiltrosProduto filtros);
        List<Produto> BuscarPorCentro(int id);
        void CadastrarProduto(Produto produto);
        void Editar(Produto produto);
    }
}
