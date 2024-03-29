﻿using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Produtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Domain.Subcategorias
{
    public class Subcategoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage =
            "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataModificacao { get; set; }
        [JsonIgnore]
        public virtual Categoria Categoria { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}
