using AutoMapper;
using EcommerceAPI.Application.Commands.Subcategorias;
using EcommerceAPI.Application.Handlers.Subcategorias;
using EcommerceAPI.Domain.Queries;
using EcommerceAPI.Domain.Repository;
using EcommerceAPI.Domain.Subcategorias;
using EcommerceAPI.Test.Application.Handlers.SubcategoriaCommandFactory;
using EcommerceAPI.Test.Domain.Entities.Factory;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace EcommerceAPI.Test.Application.Handlers
{
    public class SubcategoriaHandlerTests
    {
        private readonly Mock<ISubcategoriaQueries> _subcategoriaQueriesMock;
        private readonly Mock<ISubcategoriaRepository> _subcategoriaRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private SubcategoriaHandler _subcategoriaHandler;

        public SubcategoriaHandlerTests()
        {
            _subcategoriaQueriesMock = new Mock<ISubcategoriaQueries>();
            _subcategoriaRepositoryMock = new Mock<ISubcategoriaRepository>();
            _mapperMock = new Mock<IMapper>();
            _subcategoriaHandler = CreateSubcategoriaHandler();
        }

        private SubcategoriaHandler CreateSubcategoriaHandler()
        {
            return new SubcategoriaHandler(
                _subcategoriaRepositoryMock.Object,
                _subcategoriaQueriesMock.Object,
                _mapperMock.Object
                );
        }

        [Fact(DisplayName = "Deve ser possível cadastrar uma subcategoria")]
        public async void Deve_Criar_Subcategoria_Valida()
        {
            // Arrange
            var subcategoria = SubcategoriaFactory.Create(2);

            _mapperMock.Setup(x => x.Map<Subcategoria>(It.IsAny<CreateSubcategoriaCommand>()))
                .Returns(subcategoria);
            _subcategoriaRepositoryMock.Setup(x => x.CriarSubcategoria(It.IsAny<Subcategoria>()))
                .Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(CreateSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Subcategoria cadastrada com sucesso.", result.Message);           
        }

        [Fact(DisplayName = "Deve ser possível cadastrar uma subcategoria com retorno nulo")]
        public async void Deve_Criar_Subcategoria_Nula()
        {
            // Arrange
            var subcategoria = SubcategoriaFactory.Create(2);

            _mapperMock.Setup(x => x.Map<Subcategoria>(It.IsAny<CreateSubcategoriaCommand>()))
                .Returns(subcategoria);
            _subcategoriaRepositoryMock.Setup(x => x.CriarSubcategoria(It.IsAny<Subcategoria>()))
                .Returns<Subcategoria>(null);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(CreateSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Não foi possível cadastrar a subcategoria.", result.Message);
        }

        [Fact(DisplayName = "Deve ser possível editar uma subcategoria")]
        public async void Deve_Editar_Subcategoria_Valida()
        {
            // Arrange
            Subcategoria subcategoria = SubcategoriaFactory.Create(2);

            _subcategoriaQueriesMock.Setup(x => x.RecuperarSubcategoriaPorId(It.IsAny<int>())).Returns(Task.FromResult(subcategoria));
            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateSubcategoriaCommand>(), It.IsAny<Subcategoria>()))
                .Returns(subcategoria);
            _subcategoriaRepositoryMock.Setup(x => x.EditarSubcategoria(It.IsAny<Subcategoria>())).Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(UpdateSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Subcategoria atualizada com sucesso!", result.Message);

        }

        [Fact(DisplayName = "Não deve ser possível editar uma subcategoria não localizada")]
        public async void Nao_Deve_Editar_Subcategoria_Invalida()
        {
            // Arrange
            Subcategoria subcategoria = null;

            _subcategoriaQueriesMock.Setup(x => x.RecuperarSubcategoriaPorId(It.IsAny<int>())).Returns(Task.FromResult(subcategoria));
            _mapperMock.Setup(x => x.Map(It.IsAny<UpdateSubcategoriaCommand>(), It.IsAny<Subcategoria>()))
                .Returns(subcategoria);
            _subcategoriaRepositoryMock.Setup(x => x.EditarSubcategoria(It.IsAny<Subcategoria>())).Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(UpdateSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Subcategoria não localizada", result.Message);
        }

        [Fact(DisplayName = "Deve ser possível editar o status de uma subcategoria")]
        public async void Deve_Editar_Status_Subcategoria_Valida()
        {
            // Arrange
            Subcategoria subcategoria = SubcategoriaFactory.Create(2);
            _subcategoriaQueriesMock.Setup(x => x.RecuperarSubcategoriaPorId(It.IsAny<int>())).Returns(Task.FromResult(subcategoria));
            _subcategoriaRepositoryMock.Setup(x => x.EditarSubcategoria(It.IsAny<Subcategoria>())).Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(
                UpdateStatusSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("Status da subcategoria atualizado com sucesso!", result.Message);

        }

        [Fact(DisplayName = "Não deve ser possível editar o status de subcategoria não localizada")]
        public async void Nao_Deve_Editar_Status_Subcategoria_Invalida()
        {
            // Arrange
            Subcategoria subcategoria = null;
            _subcategoriaQueriesMock.Setup(x => x.RecuperarSubcategoriaPorId(It.IsAny<int>())).Returns(Task.FromResult(subcategoria));
            _subcategoriaRepositoryMock.Setup(x => x.EditarSubcategoria(It.IsAny<Subcategoria>())).Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(
                UpdateStatusSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Subcategoria não localizada", result.Message);
        }

        [Fact(DisplayName = "Deve ser possível tornar o status de subcategoria false")]
        public async void Alterar_Status_Subcategoria_Para_False()
        {
            // Arrange
            Subcategoria subcategoria = SubcategoriaFactory.CreateWithStatusFalse(2);
            _subcategoriaQueriesMock.Setup(x => x.RecuperarSubcategoriaPorId(It.IsAny<int>())).Returns(Task.FromResult(subcategoria));
            _subcategoriaRepositoryMock.Setup(x => x.EditarSubcategoria(It.IsAny<Subcategoria>())).Returns(subcategoria);

            CancellationToken cancellationToken = default;

            // Act
            var result = await _subcategoriaHandler.Handle(
                UpdateStatusSubcategoriaCommandFactory.Create(), cancellationToken);

            // Assert
            Assert.True(subcategoria.Status);
        }

    }
}
