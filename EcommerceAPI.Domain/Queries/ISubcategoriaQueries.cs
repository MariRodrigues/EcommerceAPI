using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Domain.Subcategorias;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Queries
{
    public interface ISubcategoriaQueries
    {
        Task<IEnumerable<ReadSubcategoriaDto>> RecuperarSubcategoriaFilter(SubcategoriaFiltros filtros);
        Task<Subcategoria> RecuperarSubcategoriaPorId(int id);
    }
}
