using EcommerceAPI.Application.Commands.Produtos;
using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Produtos
{
    public interface IProdutoHandler : 
        IRequestHandler<CreateProdutoCommand, ResponseApi>,
        IRequestHandler<UpdateProdutoCommand, ResponseApi>
    {
    }
}
