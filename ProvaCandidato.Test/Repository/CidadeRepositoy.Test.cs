using Moq;
using ProvaCandidato.Data;
using ProvaCandidato.Data.Entidade;
using ProvaCandidato.Repository;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Xunit;

namespace ProvaCandidato.Test.Repository
{
    public class CidadeRepositoyTest
    {

        [Theory]
        [InlineData("João")]
        [InlineData("Pedro Alcantra")]
        [InlineData("Maria Aparecida")]
        public void Test_Sucess_GetName(string nome)
        {
            //Arrange
            var data = new List<Cidade> { new Cidade {Nome = nome } }.AsQueryable();
            var mockContext = new Mock<ContextoPrincipal>();
            var mockSet = new Mock<DbSet<Cidade>>();

            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Set<Cidade>()).Returns(mockSet.Object);

            //Act
            var repository = new Mock<CidadesRepository>(mockContext.Object);
            var countExpected = 1;

            var result = repository.Object.GetName(nome);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(countExpected,result.Count());
        }

        [Theory]
        [InlineData("João")]
        [InlineData("Pedro Alcantra")]
        [InlineData("Maria Aparecida")]
        public void Test_Error_GetName(string nome)
        {
            //Arrange
            var data = new List<Cidade> { new Cidade { Nome = nome } }.AsQueryable();
            var mockContext = new Mock<ContextoPrincipal>();
            var mockSet = new Mock<DbSet<Cidade>>();
            var nomeFilter = "Roberval - Não foi Cadastrado";

            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Set<Cidade>()).Returns(mockSet.Object);

            //Act
            var repository = new Mock<CidadesRepository>(mockContext.Object);
            var countExpected = 2;

            var result = repository.Object.GetName(nomeFilter);

            //Assert
            Assert.Empty(result);
            Assert.NotEqual(countExpected, result.Count());
        }

        [Fact]
              
        public void Test_Sucess_GetById()
        {
            //Arrange
            var data = new List<Cidade> { new Cidade { Nome = "São Paulo" } }.AsQueryable();
            var mockContext = new Mock<ContextoPrincipal>();
            var mockSet = new Mock<DbSet<Cidade>>();
            var Id = 0;

            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Set<Cidade>()).Returns(mockSet.Object);

            //Act
            var repository = new Mock<CidadesRepository>(mockContext.Object);
            var nameExpected = "São Paulo";

            var result = repository.Object.GetById(Id);

            //Assert
            Assert.NotNull(result);
            Assert.Contains(nameExpected, result.Nome.ToString());
        }

        [Fact]
        public void Test_Error_GetById()
        {
            //Arrange
            var data = new List<Cidade> { new Cidade { Nome = "São Paulo" } }.AsQueryable();
            var mockContext = new Mock<ContextoPrincipal>();
            var mockSet = new Mock<DbSet<Cidade>>();
            var Id = 0;
            var idFilter = 1;

            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Cidade>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockContext.Setup(m => m.Set<Cidade>()).Returns(mockSet.Object);

            //Act
            var repository = new Mock<CidadesRepository>(mockContext.Object);
            var nameExpected = "Curitiba";

            var result = repository.Object.GetById(idFilter);

            //Assert
            Assert.Null(result);           
        }

    }

}
