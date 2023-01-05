using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Subcategorias;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subcategoria>()
                .HasOne(subcategoria => subcategoria.Categoria)
                .WithMany(categoria => categoria.Subcategorias)
                .HasForeignKey(subcategoria =>subcategoria.CategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.Categoria)
                .WithMany(categoria => categoria.Produtos)
                .HasForeignKey(produto => produto.CategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.Subcategoria)
                .WithMany(subcategoria => subcategoria.Produtos)
                .HasForeignKey(produto => produto.SubcategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.CentroDistribuicao)
                .WithMany(centros => centros.Produtos)
                .HasForeignKey(produto => produto.CentroDistribuicaoId);

            builder.Entity<CentroDistribuicao>()
                .HasIndex(c => c.Nome)
                .IsUnique();
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CentroDistribuicao> CentrosDistribuicao { get; set; }
 
    }
}
