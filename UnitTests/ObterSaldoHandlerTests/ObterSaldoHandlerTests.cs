using Xunit;
using NSubstitute;
using Questao5.Application.Queries;
using Questao5.Repositories;
using Questao5.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Questao5.UnitTests.ObterSaldoHandlerTests
{
	public class ObterSaldoHandlerTests
	{
		private readonly IContaCorrenteRepository _repository;
		private readonly ObterSaldoHandler _handler;

		public ObterSaldoHandlerTests()
		{
			_repository = Substitute.For<IContaCorrenteRepository>();
			_handler = new ObterSaldoHandler(_repository);
		}

		[Fact]
		public async Task Should_ReturnCorrectBalance_When_AccountExists()
		{
			// Arrange
			_repository.ContaExiste("123").Returns(Task.FromResult(true));
			_repository.ContaAtiva("123").Returns(Task.FromResult(true));
			_repository.ObterDadosConta("123").Returns(Task.FromResult((123, "Cliente Teste")));
			_repository.ObterSaldo("123").Returns(Task.FromResult(500.00m));

			var query = new ObterSaldoQuery { IdContaCorrente = "123" };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			_repository.Received(1).ObterSaldo("123");
			Assert.Equal(123, result.NumeroConta);
			Assert.Equal("Cliente Teste", result.NomeTitular);
			Assert.Equal(500.00m, result.Saldo);
		}
	}
}
