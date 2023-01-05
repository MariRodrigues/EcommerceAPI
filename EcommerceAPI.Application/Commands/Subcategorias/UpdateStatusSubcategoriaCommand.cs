using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Commands.Subcategorias
{
    public class UpdateStatusSubcategoriaCommand : IRequest<ResponseApi>
    {
        public int Id { get; set; }
    }
}
