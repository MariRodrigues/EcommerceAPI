using AutoMapper;
using EcommerceAPI.Application.Commands.Categorias;
using EcommerceAPI.Application.Handlers.Categorias;
using EcommerceAPI.Domain.Categorias;
using EcommerceAPI.Domain.Produtos;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Test.Application.Handlers.CategoriaCommandFactory;
using EcommerceAPI.Test.Domain.Entities.Factory;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace EcommerceAPI.Test.Application.Handlers
{
    public class CategoriaHandlerTests
    {
        private readonly Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly Mock<ISubcategoriaRepository> _subcategoriaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoriaHandlerTests()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _subcategoriaRepositoryMock = new Mock<ISubcategoriaRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        private CategoriaHandler CreateCategoriaHandler()
        {
            return new CategoriaHandler(
                _categoriaRepositoryMock.Object,
                _mapperMock.Object,
                _produtoRepositoryMock.Object,
                _subcategoriaRepositoryMock.Object);
        }

        [Fact(DisplayName = "Deve ser possível cadastrar uma categoria")]
        public async void Deve_Criar_Categoria_Valido()
        {
            // Arrange
            var command = CreateCategoriaCommandFactory.Create();
            var categoria = CategoriaFactory.Create();

            _mapperMock.Setup(x => x.Map<Categoria>(It.IsAny<CreateCategoriaCommand>()))
                .Returns(categoria);
            _categoriaRepositoryMock.Setup(x => x.CadastrarCategoria(It.IsAny<Categoria>()))
                .Returns(categoria);

            var categoriaHandler = CreateCategoriaHandler();
            CancellationToken cancellationToken = default;         

            // Act
            var result = await categoriaHandler.Handle(command, cancellationToken);

            // Assert
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Não deve ser possível cadastrar uma categoria com retorno nulo")]
        public async void Não_Deve_Criar_Categoria_Invalida()
        {
            // Arrange
            _categoriaRepositoryMock.Setup(x => x.CadastrarCategoria(It.IsAny<Categoria>())).Returns<Categoria>(null);

            var categoriaHandler = CreateCategoriaHandler();
            CancellationToken cancellationToken = default;

            // Act
            var result = await categoriaHandler.Handle(CreateCategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.False(result.Success);
        }

        [Fact(DisplayName = "Deve ser possível atualizar uma categoria existente")]
        public async void Testa_Atualiza_Categoria()
        {
            // Arrange
            Categoria categoria = CategoriaFactory.Create();

            _categoriaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(categoria);
            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateCategoriaCommand>(), It.IsAny<Categoria>()))
                .Returns(categoria);
            _categoriaRepositoryMock.Setup(x => x.EditarCategoria(It.IsAny<Categoria>())).Returns(categoria);

            CancellationToken cancellationToken = default;

            var categoriaHandler = CreateCategoriaHandler();

            // Act
            var result = await categoriaHandler.Handle(UpdateCategoriaCommandFactory.UpdateCategoriaCommand(), cancellationToken);

            //Assert
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Não deve ser possível atualizar uma categoria pois é inexistente (nula)")]
        public async void Testa_Atualiza_Categoria_Inexistente()
        {
            // Arrange
            Categoria categoria = null;

            _categoriaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(categoria);
            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateCategoriaCommand>(), It.IsAny<Categoria>()))
                .Returns(categoria);
            _categoriaRepositoryMock.Setup(x => x.EditarCategoria(It.IsAny<Categoria>())).Returns(categoria);

            UpdateCategoriaCommand request = UpdateCategoriaCommandFactory.UpdateCategoriaCommand();
            CancellationToken cancellationToken = default;

            var categoriaHandler = CreateCategoriaHandler();

            // Act
            var result = await categoriaHandler.Handle(request, cancellationToken);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("Categoria não localizada", result.Message);
        }

        [Fact(DisplayName = "Deve ser possível atualizar status de categoria")]
        public async void Deve_Ser_Possivel_Atualizar_Status_Categoria()
        {
            // Arrange
            Categoria categoria = CategoriaFactory.Create();
            List<Produto> listaProdutos = new();
            List<Subcategoria> listaSubcategorias = new();

            _categoriaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(categoria);
            _produtoRepositoryMock.Setup(x => x.GetAll()).Returns(listaProdutos);
            _subcategoriaRepositoryMock.Setup(x => x.GetAll()).Returns(listaSubcategorias);
            _categoriaRepositoryMock.Setup(x => x.EditarCategoria(It.IsAny<Categoria>())).Returns(categoria);

            UpdateStatusCategoriaCommand request = UpdateStatusCategoriaCommandFactory.UpdateStatusCategoriaCommand();
            CancellationToken cancellationToken = default;

            var categoriaHandler = CreateCategoriaHandler();

            // Act
            var result = await categoriaHandler.Handle(request, cancellationToken);

            //Assert
            Assert.True(result.Success);
        }

        [Fact(DisplayName = "Não deve ser possível atualizar status de categoria inexistente")]
        public async void Não_Deve_Atualizar_Status_Categoria_Nao_Localizada()
        {
            // Arrange
            Categoria categoria = CategoriaFactory.Create();
            List<Produto> listaProdutos = new();
            List<Subcategoria> listaSubcategorias = new();

            _categoriaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns<Categoria>(null);
            _produtoRepositoryMock.Setup(x => x.GetAll()).Returns(listaProdutos);
            _subcategoriaRepositoryMock.Setup(x => x.GetAll()).Returns(listaSubcategorias);
            _categoriaRepositoryMock.Setup(x => x.EditarCategoria(It.IsAny<Categoria>())).Returns(categoria);

            UpdateStatusCategoriaCommand request = UpdateStatusCategoriaCommandFactory.UpdateStatusCategoriaCommand();
            CancellationToken cancellationToken = default;

            var categoriaHandler = CreateCategoriaHandler();

            // Act
            var result = await categoriaHandler.Handle(request, cancellationToken);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("Categoria não localizada", result.Message);
        }

        [Fact(DisplayName = "Não deve ser possível inativar uma categoria com produtos inclusos")]
        public async void Não_Deve_Inativar_Categoria_Com_Produtos()
        {
            // Arrange
            Categoria categoria = CategoriaFactory.Create();
            List<Produto> listaProdutos = ProdutoFactory.Create(2);
            List<Subcategoria> listaSubcategorias = new();

            _categoriaRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(categoria);
            _produtoRepositoryMock.Setup(x => x.GetAll()).Returns(listaProdutos);
            _subcategoriaRepositoryMock.Setup(x => x.GetAll()).Returns(listaSubcategorias);
            _categoriaRepositoryMock.Setup(x => x.EditarCategoria(It.IsAny<Categoria>())).Returns(categoria);

            UpdateStatusCategoriaCommand request = UpdateStatusCategoriaCommandFactory.UpdateStatusCategoriaCommand();
            CancellationToken cancellationToken = default;

            var categoriaHandler = CreateCategoriaHandler();

            // Act
            var result = await categoriaHandler.Handle(request, cancellationToken);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("Há produtos cadastrados, não é possível inativar a categoria", result.Message);

        }

    }
}
