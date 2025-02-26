using MediatR;
using Questao5.Infrastructure.Persistence;
using Questao5.Models;
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
				return new ErrorResponse { TipoErro = "INVALID_ACCOUNT", Mensagem = "Conta não encontrada." };

			if (!await _repository.ContaAtiva(request.IdContaCorrente))
				return new ErrorResponse { TipoErro = "INACTIVE_ACCOUNT", Mensagem = "Conta inativa." };

			await _repository.RegistrarMovimento(request);
			return new { Sucesso = true };
		}
	}
}
