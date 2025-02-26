using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using MediatR;
using Questao5.Controllers;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Questao5.Models;
using System.Threading.Tasks;
using Xunit;

public class ContaCorrenteControllerTests
{
    [Fact]
    public async Task Should_ReturnSuccess_When_ValidTransactionIsPerformed()
    {
        //Arrange
        var mediator = Substitute.For<IMediator>();
        mediator.Send(Arg.Any<MovimentarContaCommand>()).Returns(Task.FromResult<object>(new { Sucesso = true }));

        var controller = new ContaCorrenteController(mediator);
        var command = new MovimentarContaCommand { IdContaCorrente = "123", Valor = 100, TipoMovimento = "C" };

        //Act
        var result = await controller.MovimentarConta(command);

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True(((dynamic)okResult.Value).Sucesso);
    }

    [Fact]
    public async Task Should_ReturnCorrectBalance_When_AccountExists()
    {
        //Arrange
        var mediator = Substitute.For<IMediator>();
        mediator.Send(Arg.Any<ObterSaldoQuery>()).Returns(Task.FromResult(new SaldoResponse
        {
            NumeroConta = 123,
            NomeTitular = "Cliente Teste",
            Saldo = 500.00m
        }));

        var controller = new ContaCorrenteController(mediator);

        //Act
        var result = await controller.ConsultarSaldo("123");

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var saldoResponse = Assert.IsType<SaldoResponse>(okResult.Value);
        Assert.Equal(500.00m, saldoResponse.Saldo);
        Assert.Equal("Cliente Teste", saldoResponse.NomeTitular);
    }
}
