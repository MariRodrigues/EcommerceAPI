using AutoMapper;
using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Application.Response;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using System.Threading;
using System.Threading.Tasks;

namespace EcommerceAPI.Application.Handlers.Subcategorias
{
    public class SubcategoriaHandler : ISubcategoriaHandler
    {
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ISubcategoriaQueries _subcategoriaQueries;
        private readonly IMapper _mapper;

        public SubcategoriaHandler(ISubcategoriaRepository subcategoriaRepository, ISubcategoriaQueries subcategoriaQueries, IMapper mapper)
        {
            _subcategoriaRepository = subcategoriaRepository;
            _subcategoriaQueries = subcategoriaQueries;
            _mapper = mapper;
        }

        public async Task<ResponseApi> Handle(CreateSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = _mapper.Map<Subcategoria>(request);
            var response = _subcategoriaRepository.CriarSubcategoria(subcategoria);

            if (response == null)
                return new ResponseApi(false, "Não foi possível cadastrar a subcategoria.");

            return new ResponseApi(true, "Subcategoria cadastrada com sucesso.")
            {
                Id = subcategoria.Id
            };
        }

        public async Task<ResponseApi> Handle(UpdateSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = await _subcategoriaQueries.RecuperarSubcategoriaPorId(request.Id);

            if (subcategoria == null)
                return new ResponseApi(false, "Subcategoria não localizada");

            _mapper.Map(request, subcategoria);
            _subcategoriaRepository.EditarSubcategoria(subcategoria);

            return new ResponseApi(true, "Subcategoria atualizada com sucesso!")
            {
                Id = subcategoria.Id
            };
        }

        public async Task<ResponseApi> Handle(UpdateStatusSubcategoriaCommand request, CancellationToken cancellationToken)
        {
            Subcategoria subcategoria = await _subcategoriaQueries.RecuperarSubcategoriaPorId(request.Id);

            if (subcategoria == null)
                return new ResponseApi(false, "Subcategoria não localizada");

            if (subcategoria.Status)
                subcategoria.Status = false;
            else
                subcategoria.Status = true;

            _subcategoriaRepository.EditarSubcategoria(subcategoria);

            return new ResponseApi(true, "Status da subcategoria atualizado com sucesso!")
            {
                Id = subcategoria.Id
            };
        }
    }
}
