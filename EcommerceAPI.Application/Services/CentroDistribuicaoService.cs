using AutoMapper;
using EcommerceAPI.Domain.Centros;
using EcommerceAPI.Domain.Centros.DTO;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Repository;
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
        private readonly ICentroDistribuicaoRepository _centroRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICentroQueries _centroQueries;

        static readonly HttpClient client = new();
        public CentroDistribuicaoService(
            IMapper mapper, 
            ICentroDistribuicaoRepository centroRepository, 
            IProdutoRepository produtoRepository, 
            ICentroQueries centroQueries)
        {
            _mapper = mapper;
            _centroRepository = centroRepository;
            _produtoRepository = produtoRepository;
            _centroQueries = centroQueries;
        }

        public IEnumerable<CentroDistribuicao> PesquisarCentros(FiltrosCD filtros)
        {
            return _centroQueries.GetAllFilter(filtros);
        }
    }
}
