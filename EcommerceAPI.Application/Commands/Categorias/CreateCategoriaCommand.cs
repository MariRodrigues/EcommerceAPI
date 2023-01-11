using EcommerceAPI.Application.Response;
using MediatR;

namespace EcommerceAPI.Application.Commands.Categorias
{
    public class CreateCategoriaCommand : IRequest<ResponseApi>
    {
        public string Nome { get; set; }
    }
}
