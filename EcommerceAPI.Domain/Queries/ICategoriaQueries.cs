using EcommerceAPI.Domain.Categorias.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Queries
{
    public interface ICategoriaQueries
    {
        Task<IEnumerable<ReadCategoriaDto>> GetAllFilter(FiltrosCategoria filtros);
    }
}
