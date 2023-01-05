using System;
using System.Collections.Generic;

namespace EcommerceAPI.Domain.Categorias.DTO
{
    public class ReadCategoriaDto
    {
        public int CategoriaId { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public List<Subcategorias> Subcategorias { get; set; }
        public List<Produtos> Produtos { get; set; }

        public override string ToString()
        {
            var response = $"Nome: {Nome}, Status: {Status}, Data de criação: {DataCriacao}";
            return response;
        }
    }

    public class Subcategorias
    {
        public int SubcategoriaId { get; set; }
        public string Nome { get; set; }
    }

    public class Produtos
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; }
    }
}
