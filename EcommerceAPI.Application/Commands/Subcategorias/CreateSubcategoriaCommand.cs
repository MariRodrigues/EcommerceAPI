using EcommerceAPI.Application.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;


namespace EcommerceAPI.Application.Commands.Subcategorias
{
    public class CreateSubcategoriaCommand : IRequest<ResponseApi>
    {
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        public int CategoriaId { get; set; }
    }
}
