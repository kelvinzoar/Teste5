using NSubstitute;
using Questao5.Repositories;
using Questao5.Application.Commands;
using Questao5.Application.Errors;
using Questao5.Models;
using System.Threading.Tasks;
using Xunit;

public class ContaCorrenteTests
{
    [Fact]
    public async Task MovimentarConta_ComContaInexistente_DeveRetornarErro()
    {
        //Arrange
        var repo = Substitute.For<IContaCorrenteRepository>();
        repo.ContaExiste("123").Returns(Task.FromResult(false));

        var handler = new MovimentarContaHandler(repo);

        //Act
        var response = await handler.Handle(new MovimentarContaCommand { IdContaCorrente = "123" }, default);

        //Assert
        Assert.IsType<ErrorResponse>(response);
    }

}
