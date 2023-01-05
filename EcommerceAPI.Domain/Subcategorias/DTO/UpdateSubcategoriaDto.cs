using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Domain.Subcategorias.DTO
{
    public class UpdateSubcategoriaDto
    {
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
    }
}
