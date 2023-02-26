﻿// <auto-generated />
using System;
using EcommerceAPI.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcommerceAPI.Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230225161051_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("EcommerceAPI.Domain.Categorias.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataModificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Centros.CentroDistribuicao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasColumnType("text");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Logradouro")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UF")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("CentrosDistribuicao");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Produtos.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Altura")
                        .HasColumnType("double");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CentroDistribuicaoId")
                        .HasColumnType("int");

                    b.Property<double>("Comprimento")
                        .HasColumnType("double");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataModificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("varchar(512)");

                    b.Property<double>("Largura")
                        .HasColumnType("double");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<double>("Peso")
                        .HasColumnType("double");

                    b.Property<int>("QuantidadeEstoque")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("SubcategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroDistribuicaoId");

                    b.HasIndex("SubcategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Subcategorias.Subcategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataModificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Subcategorias");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Produtos.Produto", b =>
                {
                    b.HasOne("EcommerceAPI.Domain.Categorias.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Domain.Centros.CentroDistribuicao", "CentroDistribuicao")
                        .WithMany("Produtos")
                        .HasForeignKey("CentroDistribuicaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Domain.Subcategorias.Subcategoria", "Subcategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubcategoriaId");

                    b.Navigation("Categoria");

                    b.Navigation("CentroDistribuicao");

                    b.Navigation("Subcategoria");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Subcategorias.Subcategoria", b =>
                {
                    b.HasOne("EcommerceAPI.Domain.Categorias.Categoria", "Categoria")
                        .WithMany("Subcategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Categorias.Categoria", b =>
                {
                    b.Navigation("Produtos");

                    b.Navigation("Subcategorias");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Centros.CentroDistribuicao", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("EcommerceAPI.Domain.Subcategorias.Subcategoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
