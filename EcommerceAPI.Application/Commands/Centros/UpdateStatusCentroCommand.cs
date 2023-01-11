using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Commands.Centros
{
    public class UpdateStatusCentroCommand : IRequest<ResponseApi>
    {
        [Required]
        public int Id { get; set; }
    }
}
