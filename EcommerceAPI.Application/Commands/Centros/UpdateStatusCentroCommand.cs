using EcommerceAPI.Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Application.Commands.Centros
{
    public class UpdateStatusCentroCommand : IRequest<ResponseApi>
    {
        [Required]
        public int Id { get; set; }
    }
}
