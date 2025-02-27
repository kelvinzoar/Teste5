using MediatR;
using Questao5.Repositories;
using Questao5.Models;
using Questao5.Application.Errors;
using System.Threading;
using System.Threading.Tasks;

namespace Questao5.Application.Commands
{
	public class MovimentarContaHandler : IRequestHandler<MovimentarContaCommand, object>
	{
		private readonly IContaCorrenteRepository _repository;

		public MovimentarContaHandler(IContaCorrenteRepository repository)
		{
			_repository = repository;
		}

		public async Task<object> Handle(MovimentarContaCommand request, CancellationToken cancellationToken)
		{
			if (!await _repository.ContaExiste(request.IdContaCorrente))
				return new ErrorResponse("Conta não encontrada.", ErrorCodes.INVALID_ACCOUNT);

			if (!await _repository.ContaAtiva(request.IdContaCorrente))
				return new ErrorResponse("Conta inativa.", ErrorCodes.INACTIVE_ACCOUNT);

			var movimentacao = new MovimentacaoRequest
			{
				ChaveIdempotencia = request.ChaveIdempotencia,
				IdContaCorrente = request.IdContaCorrente,
				TipoMovimento = request.TipoMovimento,
				Valor = request.Valor
			};

			await _repository.RegistrarMovimento(movimentacao);

			return new { Sucesso = true };
		}
	}
}
