using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Infra.Data;
using System;
using System.Linq;

namespace EcommerceAPI.Infra.Repository
{
    public class CentroDistribuicaoRepository : ICentroDistribuicaoRepository
    {
        private readonly AppDbContext _context;

        public CentroDistribuicaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public CentroDistribuicao Cadastrar(CentroDistribuicao centro)
        {
            _context.CentrosDistribuicao.Add(centro);
            _context.SaveChanges();
            return centro;
        }

        public CentroDistribuicao GetById(int id)
        {
            return _context.CentrosDistribuicao.FirstOrDefault(c => c.Id == id);
        }

        public void EditarStatusCentro (CentroDistribuicao centro)
        {
            if (centro.Status)
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

        public CentroDistribuicao EditarCentro(CentroDistribuicao centroAtualizar)
        {
            centroAtualizar.DataModificacao = DateTime.Now;
            _context.Update(centroAtualizar);
            _context.SaveChanges();
            return centroAtualizar;
        }
    }
}
