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
    public class UpdateProdutoCommand :IRequest<ResponseApi>
    {
        [Required]
        public int Id { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(512, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Descricao { get; set; }
        public bool Status { get; set; } = true;
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int CentroDistribuicaoId { get; set; }

        public int CategoriaId { get; set; }

        public int SubcategoriaId { get; set; }
    }
}
