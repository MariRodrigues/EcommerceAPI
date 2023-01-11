using EcommerceAPI.Domain.Centros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Domain.Repository
{
    public interface ICentroDistribuicaoRepository
    {
        CentroDistribuicao Cadastrar(CentroDistribuicao centro);
        public CentroDistribuicao GetById(int id);
        void EditarStatusCentro(CentroDistribuicao centro);
        CentroDistribuicao EditarCentro(CentroDistribuicao centroAtualizar);

    }
}
