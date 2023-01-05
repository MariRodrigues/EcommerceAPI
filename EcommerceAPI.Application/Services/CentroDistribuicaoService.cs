using AutoMapper;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Services
{
    public class CentroDistribuicaoService
    {
        private readonly IMapper _mapper;
        private readonly CentroDistribuicaoRepository _centroRepository;
        private readonly ProdutoRepository _produtoRepository;
        private readonly CentroQueries _centroQueries;

        static readonly HttpClient client = new();
        public CentroDistribuicaoService(IMapper mapper, CentroDistribuicaoRepository centroRepository, ProdutoRepository produtoRepository, CentroQueries centroQueries)
        {
            _mapper = mapper;
            _centroRepository = centroRepository;
            _produtoRepository = produtoRepository;
            _centroQueries = centroQueries;
        }

        public async Task<CentroDistribuicao> CadastrarCentro (CreateCentroDto createDto)
        {
            var centroJson = await RetornarEnderecoViaCEP(createDto.CEP);

            CentroDistribuicao centro = _mapper.Map<CentroDistribuicao>(centroJson);
            _mapper.Map(createDto, centro);
            centro.Cidade = centroJson.Localidade;

            if (!EnderecoEhUnico(centro))
            {
                throw new ArgumentException("CD já existe! Mesmo logradouro, número e complemento.");
            }
            _centroRepository.Cadastrar(centro);
            return centro;
        }

        public IEnumerable<CentroDistribuicao> PesquisarCentros(FiltrosCD filtros)
        {
            return _centroQueries.GetAllFilter(filtros);
        }

        public ReadCentroDistribuicao RecuperarCentroPorId(int id)
        {
            var centroDto = _mapper.Map<ReadCentroDistribuicao>(_centroRepository.GetById(id));
            return centroDto;
        }

        public Result EditarStatusCentro (int id)
        {
            var centro = _centroRepository.GetById(id);
            var listaProdutos = _produtoRepository.BuscarPorCentro(centro.Id);

            if (listaProdutos.Count != 0)
            {
                return Result.Fail("Não é possível modificar pois há produtos associados!");
            }

            _centroRepository.EditarStatusCentro(centro);
            return Result.Ok();
        }

        public async Task<Result> EditarCentro(int id, UpdateCentroDto centroDto)
        {
            var centroAtualizar = _centroRepository.GetById(id);

            if (centroAtualizar == null)
            {
                return Result.Fail("Centro de distribuição não localizado!");
            }

            var enderecoJson = await RetornarEnderecoViaCEP(centroDto.CEP);

            _mapper.Map(enderecoJson, centroAtualizar);
            centroAtualizar.Cidade = enderecoJson.Localidade;
            _mapper.Map(centroDto, centroAtualizar);

            _centroRepository.EditarCentro(centroAtualizar);

            return Result.Ok();
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

        private bool EnderecoEhUnico(CentroDistribuicao centro)
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
