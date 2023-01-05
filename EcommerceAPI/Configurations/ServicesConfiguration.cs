using EcommerceAPI.Application.Services;
using EcommerceAPI.Infra.Data;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace EcommerceAPI.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<CentroDistribuicaoRepository, CentroDistribuicaoRepository>();
            services.AddScoped<CentroDistribuicaoService, CentroDistribuicaoService>();
            services.AddScoped<CategoriaRepository, CategoriaRepository>();
            services.AddScoped<CategoriaService, CategoriaService>();
            services.AddScoped<SubcategoriaRepository, SubcategoriaRepository>();
            services.AddScoped<SubcategoriaService, SubcategoriaService>();
            services.AddScoped<ProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoService, ProdutoService>();
            services.AddScoped<CentroQueries, CentroQueries>();
            services.AddScoped<CategoriaQueries, CategoriaQueries>();
            services.AddScoped<SubcategoriaQueries, SubcategoriaQueries>();
            services.AddScoped<ProdutoQueries>();

            var assembly = AppDomain.CurrentDomain.Load("EcommerceAPI.Application");
            services.AddMediatR(assembly);

            services.AddDbContext<AppDbContext>(opts => opts.UseLazyLoadingProxies()
            .UseMySQL(Configuration.GetConnectionString("CategoriaConnection")));
            
            services.AddControllers();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            

            return services;
        }
    }
}
