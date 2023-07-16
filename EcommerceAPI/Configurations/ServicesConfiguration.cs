using EcommerceAPI.Application.Services;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Data;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EcommerceAPI.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ICentroDistribuicaoRepository, CentroDistribuicaoRepository>();
            services.AddScoped<CentroDistribuicaoService>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<ISubcategoriaRepository, SubcategoriaRepository>();
            services.AddScoped<SubcategoriaService>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<ICentroQueries, CentroQueries>();
            services.AddScoped<ICategoriaQueries,CategoriaQueries>();
            services.AddScoped<ISubcategoriaQueries, SubcategoriaQueries>();
            services.AddScoped<IProdutoQueries, ProdutoQueries>();

            var assembly = AppDomain.CurrentDomain.Load("EcommerceAPI.Application");
            services.AddMediatR(assembly);

            var connectionString = Environment.GetEnvironmentVariable("CategoriaConnection");

            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies()
            .UseMySQL(connectionString));
            
            services.AddControllers();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);          

            return services;
        }
    }
}
