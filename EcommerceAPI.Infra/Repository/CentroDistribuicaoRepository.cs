using Dapper;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Infra.Data;
using FluentResults;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Infra.Repository
{
    public class CentroDistribuicaoRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CentroDistribuicaoRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Result Cadastrar(CentroDistribuicao centro)
        {
            _context.CentrosDistribuicao.Add(centro);
            _context.SaveChanges();
            return Result.Ok();
        }

        public CentroDistribuicao GetById(int id)
        {
            return _context.CentrosDistribuicao.FirstOrDefault(c => c.Id == id);
        }

        public void EditarStatusCentro (CentroDistribuicao centro)
        {
            if (centro.Status == true)
            {
                centro.Status = false;
                centro.DataModificacao = DateTime.Now;
            }
            else
            {
                centro.Status = true;
                centro.DataModificacao = DateTime.Now;
            }
            _context.SaveChanges();
        }

        public Result EditarCentro(CentroDistribuicao centroAtualizar)
        {
            _context.Update(centroAtualizar);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
