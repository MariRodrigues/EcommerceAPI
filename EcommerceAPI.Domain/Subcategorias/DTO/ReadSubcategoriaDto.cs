using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Subcategorias.DTO
{
    public class ReadSubcategoriaDto
    {
        public int SubcategoriaId { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataModificacao { get; set; }
        public Categorias Categorias { get; set; }
        public List<Produtos> Produtos { get; set; }
    }
    public class Categorias
    {
        public int CategoriaId { get; set; }
       public string Nome { get; set; }
    }
    public class Produtos
    {
        public int ProdutosId { get; set; }
        public string Nome { set; get; }
    }
}
