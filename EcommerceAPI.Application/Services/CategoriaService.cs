using EcommerceAPI.Domain.Categorias.DTO;
using EcommerceAPI.Infra.Queries;

using System.Linq;

namespace EcommerceAPI.Application.Services
{
    public class CategoriaService
    {
        private readonly CategoriaQueries _categoriaQueries;

        public CategoriaService(CategoriaQueries categoriaQueries)
        {
            _categoriaQueries = categoriaQueries;
        }
        public ReadCategoriaDto RecuperaCategoriaPorId(int id)
        {
            FiltrosCategoria filtros = new FiltrosCategoria()
            {
                Id = id
            };

            var categoriaDto = _categoriaQueries.GetAllFilter(filtros).Result.FirstOrDefault();
            return categoriaDto;
        }
    }
}
