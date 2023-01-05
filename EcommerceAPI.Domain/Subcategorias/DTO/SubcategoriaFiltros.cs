using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Subcategorias.DTO
{
    public class SubcategoriaFiltros
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Status { get; set; }
        public string Ordem { get; set; }
        public int Pagina { get; set; } = 1;
        public int ItensPagina { get; set; } = 25;

    }
}
