using NSubstitute;
using Questao5.Application.Queries;
using Questao5.Infrastructure.Persistence;
using Questao5.Models;
using System.Threading.Tasks;
using Xunit;

public class ObterSaldoHandlerTests
{
	[Fact]
	public async Task Should_ReturnCorrectBalance_When_AccountExists()
	{
		//Arrange
		var repo = Substitute.For<IContaCorrenteRepository>();
		repo.ObterSaldo("123").Returns(Task.FromResult(500.00m));
		repo.ObterDadosConta("123").Returns(Task.FromResult((123, "Cliente Teste")));

		var handler = new ObterSaldoHandler(repo);

		//Act
		var response = await handler.Handle(new ObterSaldoQuery { IdContaCorrente = "123" }, default);

		//Assert
		Assert.Equal(500.00m, response.Saldo);
		Assert.Equal("Cliente Teste", response.NomeTitular);
	}
}
