using EcommerceAPI.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Commands.Produtos
{
    public class CreateProdutoCommand : IRequest<ResponseApi>
    {
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
        [Required]
        public double Peso { get; set; }
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
        public string Url { get; set; }
        [Required]
        public int CentroDistribuicaoId { get; set; }
        public int CategoriaId { get; set; }
        public int SubcategoriaId { get; set; }
    }
}
