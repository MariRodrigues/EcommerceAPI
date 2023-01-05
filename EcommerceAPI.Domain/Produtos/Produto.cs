using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Subcategorias;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Produtos
{
    public class Produto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(512, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Descricao { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataModificacao { get; set; }
        [Required]
        public double Peso { get; set; }
        public string Url { get; set; }
        [Required]
        public double Altura { get; set; }
        [Required]
        public double Largura { get; set; }
        [Required]
        public double Comprimento { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public int QuantidadeEstoque { get; set; }
        [Required]
        [JsonIgnore]
        public virtual CentroDistribuicao CentroDistribuicao { get; set; }

        public int CentroDistribuicaoId { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }

        public int CategoriaId { get; set; }
        [JsonIgnore]
        public virtual Subcategoria Subcategoria { get; set; }

        public int? SubcategoriaId { get; set; }
    }
}
