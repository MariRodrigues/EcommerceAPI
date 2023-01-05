using System;

namespace EcommerceAPI.Domain.Produtos.DTO
{
    public class FiltrosProduto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool? Status { get; set; } = true;
        public double? Peso { get; set; }
        public double? Altura { get; set; }
        public double? Largura { get; set; }
        public double? Comprimento { get; set; }
        public double? Valor { get; set; }
        public int? QuantidadeEstoque { get; set; }
        public int Pagina { get; set; } = 1;
        public int ItensPagina { get; set; } = 25;
        public string TipoOrdem { get; set; }
        public string OrdenarPor { get; set; }
        public string Categoria { get; set; }
        public string CentroDistribuicao { get; set; }
    }
}
