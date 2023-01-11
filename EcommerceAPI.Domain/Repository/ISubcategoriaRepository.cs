using EcommerceAPI.Domain.Subcategorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Repository
{
    public interface ISubcategoriaRepository
    {
        Subcategoria CriarSubcategoria(Subcategoria subcategoria);
        Subcategoria GetById(int id);
        IEnumerable<Subcategoria> GetAll();
        Subcategoria EditarSubcategoria(Subcategoria subcategoria);
        Subcategoria EditarStatus(Subcategoria subcategoria);
        void RemoverSubcategoria(Subcategoria subcategoria);
    }
}
