using EcommerceAPI.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Centros
{
    public class CentroDistribuicao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(256, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Logradouro { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP  { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime DataModificacao { get; set; }
        public bool Status { get; set; } = true;
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}
