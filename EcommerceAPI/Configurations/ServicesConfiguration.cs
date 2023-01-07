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
            services.AddScoped<CentroDistribuicaoRepository>();
            services.AddScoped<CentroDistribuicaoService>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<CategoriaService>();
            services.AddScoped<SubcategoriaRepository>();
            services.AddScoped<SubcategoriaService>();
            services.AddScoped<ProdutoRepository>();
            services.AddScoped<ProdutoService>();
            services.AddScoped<CentroQueries>();
            services.AddScoped<CategoriaQueries>();
            services.AddScoped<SubcategoriaQueries>();
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
