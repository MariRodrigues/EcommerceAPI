using EcommerceAPI.Domain.Categorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Repository
{
    public interface ICategoriaRepository
    {
        Categoria GetById(int id);
        Categoria CadastrarCategoria(Categoria categoria);
        IEnumerable<Categoria> GetAll();
        Categoria EditarCategoria(Categoria categoriaAtualizar);
    }
}
