using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Subcategorias
{
    public interface ISubcategoriaHandler :
        IRequestHandler<CreateSubcategoriaCommand, ResponseApi>,
        IRequestHandler<UpdateSubcategoriaCommand, ResponseApi>,
        IRequestHandler<UpdateStatusSubcategoriaCommand, ResponseApi>
    {
    }
}
