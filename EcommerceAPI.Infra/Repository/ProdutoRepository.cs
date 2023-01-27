using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> GetAll()
        {
            return _context.Produtos.ToList();
        }

        public List<Produto> BuscarPorCentro(int id)
        {
            return _context.Produtos.Where(p=> p.CentroDistribuicaoId == id).ToList();
        }

        public void CadastrarProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void Editar(Produto produto)
        {
            produto.DataModificacao = DateTime.Now;
            _context.Update(produto);
            _context.SaveChanges();
        }
    }
}
