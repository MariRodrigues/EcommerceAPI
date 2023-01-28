using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Domain.Centros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Queries
{
    public interface ICentroQueries
    {
        IEnumerable<CentroDistribuicao> GetAllFilter(FiltrosCD filtros);
    }
}
