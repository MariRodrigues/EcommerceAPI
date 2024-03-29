﻿namespace EcommerceAPI.Domain.Centros.DTO
{
    public class FiltrosCD
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool? Status { get; set; }
        public string Ordem { get; set; }
        public int Pagina { get; set; } = 1;
        public int ItensPagina { get; set; } = 25;
    }
}
