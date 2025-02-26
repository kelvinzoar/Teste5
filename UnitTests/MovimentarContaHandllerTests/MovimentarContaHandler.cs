using NSubstitute;
using Questao5.Application.Commands;
using Questao5.Repositories;
using Questao5.Models;
using System.Threading.Tasks;
using Xunit;

public class MovimentarContaHandlerTests
{
	[Fact]
	public async Task Should_ReturnError_When_AccountDoesNotExist()
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

	[Fact]
	public async Task Should_ReturnError_When_AccountIsInactive()
	{
		//Arrange
		var repo = Substitute.For<IContaCorrenteRepository>();
		repo.ContaExiste("123").Returns(Task.FromResult(true));
		repo.ContaAtiva("123").Returns(Task.FromResult(false));

		var handler = new MovimentarContaHandler(repo);

		//Act
		var response = await handler.Handle(new MovimentarContaCommand { IdContaCorrente = "123" }, default);

		//Assert
		Assert.IsType<ErrorResponse>(response);
	}
}
