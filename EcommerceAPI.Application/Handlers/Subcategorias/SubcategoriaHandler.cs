using AutoMapper;
using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;
using EcommerceAPI.Infra.Queries;
using EcommerceAPI.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Subcategorias
{
    public class SubcategoriaHandler : ISubcategoriaHandler
    {
        private readonly SubcategoriaRepository _subcategoriaRepository;
        private readonly SubcategoriaQueries _subcategoriaQueries;
        private readonly IMapper _mapper;

        public SubcategoriaHandler(SubcategoriaRepository subcategoriaRepository, SubcategoriaQueries subcategoriaQueries, IMapper mapper)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _subcategoriaQueries = subcategoriaQueries;
            _mapper = mapper;
        }

        public async Task<ResponseApi> Handle(CreateSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = new()
            {
                Nome = request.Nome,
                CategoriaId = request.CategoriaId
            };
             _subcategoriaRepository.CriarSubcategoria(subcategoria);

            var response = new ResponseApi(true, "Subcategoria cadastrada com sucesso")
            {
                Id = subcategoria.Id
            };

            return response;
        }

        public async Task<ResponseApi> Handle(UpdateSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = await _subcategoriaQueries.RecuperarSubcategoriaPorId(request.Id);

            if (subcategoria == null)
                return new ResponseApi(false, "Subcategoria não localizada");

            _mapper.Map(request, subcategoria);
            _subcategoriaRepository.EditarSubcategoria(subcategoria);

            var response = new ResponseApi(true, "Subcategoria atualizada com sucesso!")
            {
                Id = subcategoria.Id
            };

            return response;
        }

        public async Task<ResponseApi> Handle(UpdateStatusSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = await _subcategoriaQueries.RecuperarSubcategoriaPorId(request.Id);

            if (subcategoria == null)
                return new ResponseApi(false, "Subcategoria não localizada");

            if (subcategoria.Status == true)
                subcategoria.Status = false;
            else
                subcategoria.Status = true;

            _subcategoriaRepository.EditarSubcategoria(subcategoria);

            var response = new ResponseApi(true, "Status da subcategoria atualizado com sucesso!")
            {
                Id = subcategoria.Id
            };

            return response;
        }
    }
}
