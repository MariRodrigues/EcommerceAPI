using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Commands.Categorias
{
    public class CreateCategoriaCommand : IRequest<ResponseApi>
    {
        public string Nome { get; set; }
    }
}
