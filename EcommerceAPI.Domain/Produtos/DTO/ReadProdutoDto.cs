
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Subcategorias;
using System;

namespace EcommerceAPI.Domain.Produtos.DTO
{
    public class ReadProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataModificacao { get; set; }

        public double Peso { get; set; }

        public double Altura { get; set; }

        public double Largura { get; set; }
        public string Url { get; set; }

        public double Comprimento { get; set; }

        public double Valor { get; set; }

        public int QuantidadeEstoque { get; set; }

        public CentroDistribuicao CentroDistribuicao { get; set; }

        public Categoria Categoria { get; set; }

        public Subcategoria Subcategoria { get; set; }

    }
}
