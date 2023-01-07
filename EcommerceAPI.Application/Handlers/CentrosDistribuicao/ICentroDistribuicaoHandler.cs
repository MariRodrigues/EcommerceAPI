using EcommerceAPI.Application.Commands.Centros;
using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.CentrosDistribuicao
{
    public interface ICentroDistribuicaoHandler :
        IRequestHandler<CreateCentroCommand, ResponseApi>
    {
    }
}
