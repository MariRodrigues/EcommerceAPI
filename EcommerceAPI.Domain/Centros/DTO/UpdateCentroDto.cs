using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Centros.DTO
{
    public class UpdateCentroDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }  
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public DateTime DataModificacao { get; set; } = DateTime.Now;

    }
}
