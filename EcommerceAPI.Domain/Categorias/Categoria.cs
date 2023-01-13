using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Subcategorias;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Categorias
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataModificacao { get; set; }
        [JsonIgnore]
        public virtual List<Subcategoria> Subcategorias { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }

    }
}
