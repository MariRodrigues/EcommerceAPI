namespace EcommerceAPI.Domain.Categorias.DTO
{
    public class FiltrosCategoria
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public bool? Status { get; set; }
        public string Ordem { get; set; }
        public int Pagina { get; set; } = 1;
        public int ItensPagina { get; set; } = 25;
    }
}
