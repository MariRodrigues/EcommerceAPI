using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Categorias
{
    public interface ICategoriaHandler : 
        IRequestHandler<CreateCategoriaCommand, ResponseApi>, 
        IRequestHandler<UpdateCategoriaCommand, ResponseApi>,
        IRequestHandler<UpdateStatusCategoriaCommand, ResponseApi>
    {
        
    }
}
