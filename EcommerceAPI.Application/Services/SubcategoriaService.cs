using AutoMapper;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Domain.Subcategorias.DTO;

namespace EcommerceAPI.Application.Services
{
    public class SubcategoriaService
    {
        private readonly IMapper _mapper;
        private readonly ISubcategoriaRepository _subcategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public SubcategoriaService(
            IMapper mapper, 
            ISubcategoriaRepository subcategoriaRepository, 
            ICategoriaRepository categoriaRepository)
        {
            _mapper = mapper;
            _subcategoriaRepository = subcategoriaRepository;
            _categoriaRepository = categoriaRepository;
        }

        public Subcategoria CadastrarSubcategoria(CreateSubcategoriaDto subcategoriaDTO)
        {
            Categoria categoria = _categoriaRepository.GetById(subcategoriaDTO.CategoriaId);

            if (categoria == null) return null;

            if (categoria.Status)
            {
                Subcategoria subcategoria = _mapper.Map<Subcategoria>(subcategoriaDTO);
                _subcategoriaRepository.CriarSubcategoria(subcategoria);
                return subcategoria;
            }
            return null;
        }
  
    }
}
