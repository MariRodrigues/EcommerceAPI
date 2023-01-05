using System;
using System.Collections.Generic;

namespace EcommerceAPI.Domain.Centros.DTO
{
    public class ReadCentroDistribuicao
    {   
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public DateTime DataCriacao { get; set; } 
        public DateTime DataModificacao { get; set; }
        public bool Status { get; set; }
        public object Produtos { get; set; }
    }
}
