using AutoMapper;
using EcommerceAPI.Application.Commands.Centros;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.CentrosDistribuicao
{
    public class CentroDistribuicaoHandler : ICentroDistribuicaoHandler
    {
        private readonly CentroQueries _centroQueries;
        private readonly IMapper _mapper;
        private readonly CentroDistribuicaoRepository _centroRepository;
        private readonly ProdutoRepository _produtoRepository;

        static readonly HttpClient client = new();

        public CentroDistribuicaoHandler(CentroQueries centroQueries, IMapper mapper, CentroDistribuicaoRepository centroRepository, ProdutoRepository produtoRepository)
        {
            _centroQueries = centroQueries;
            _mapper = mapper;
            _centroRepository = centroRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<ResponseApi> Handle(CreateCentroCommand request, CancellationToken cancellationToken)
        {
            var centroJson = await RetornarEnderecoViaCEP(request.CEP);

            CentroDistribuicao centro = _mapper.Map<CentroDistribuicao>(centroJson);
            _mapper.Map(request, centro);
            centro.Cidade = centroJson.Localidade;

            if (!AddressIsUnique(centro))
            {
                return new ResponseApi(false, "CD já existe! Mesmo logradouro, número e complemento.");
            }
            _centroRepository.Cadastrar(centro);
            return new ResponseApi(true, "CD criado com sucesso.");
        }

        public async Task<ResponseApi> Handle(UpdateCentroCommand request, CancellationToken cancellationToken)
        {
            var centroAtualizar = _centroRepository.GetById(request.Id);

            if (centroAtualizar == null)
            {
                return new ResponseApi(false, "Centro de distribuição não localizado.");
            }

            var enderecoJson = await RetornarEnderecoViaCEP(request.CEP);

            _mapper.Map(enderecoJson, centroAtualizar);
            centroAtualizar.Cidade = enderecoJson.Localidade;
            _mapper.Map(request, centroAtualizar);

            _centroRepository.EditarCentro(centroAtualizar);

            return new ResponseApi(true, "CD atualizado com sucesso.");
        }

        public async Task<ResponseApi> Handle(UpdateStatusCentroCommand request, CancellationToken cancellationToken)
        {
            var centro = _centroRepository.GetById(request.Id);
            var listaProdutos = _produtoRepository.BuscarPorCentro(centro.Id);

            if (listaProdutos.Count != 0)
            {
                return new ResponseApi(true, "Não é possível modificar pois há produtos associados.");
            }

            _centroRepository.EditarStatusCentro(centro);
            return new ResponseApi(true, "Status do CD atualizado com sucesso.");
        }

        private static async Task<EnderecoJson> RetornarEnderecoViaCEP(string cep)
        {
            string url = $"https://viacep.com.br/ws/{cep}/json/";

            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var enderecoJson = JsonSerializer.Deserialize<EnderecoJson>(responseBody, options);

            if (enderecoJson.Erro == "true")
            {
                return null;
            }

            return enderecoJson;
        }
        private bool AddressIsUnique(CentroDistribuicao centro)
        {
            FiltrosCD filtros = new();
            var listaCD = _centroQueries.GetAllFilter(filtros);

            string enderecoCompletoCD = centro.Logradouro;
            enderecoCompletoCD += centro.Numero;
            enderecoCompletoCD += centro.Complemento;

            foreach (var c in listaCD)
            {
                string enderecoCompleto = c.Logradouro;
                enderecoCompleto += c.Numero;
                enderecoCompleto += c.Complemento;

                if (enderecoCompleto == enderecoCompletoCD)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
