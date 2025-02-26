using MediatR;
using Questao5.Repositories;
using Questao5.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Questao5.Application.Queries
{
    public class ObterSaldoHandler : IRequestHandler<ObterSaldoQuery, SaldoResponse>
    {
        private readonly IContaCorrenteRepository _repository;

        public ObterSaldoHandler(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<SaldoResponse> Handle(ObterSaldoQuery request, CancellationToken cancellationToken)
        {
            if (!await _repository.ContaExiste(request.IdContaCorrente))
                return new SaldoResponse { NumeroConta = 0, NomeTitular = "Conta não encontrada", Saldo = 0 };

            var dados = await _repository.ObterDadosConta(request.IdContaCorrente);
            var saldo = await _repository.ObterSaldo(request.IdContaCorrente);

            return new SaldoResponse
            {
                NumeroConta = dados.Numero,
                NomeTitular = dados.Nome,
                DataConsulta = DateTime.UtcNow,
                Saldo = saldo
            };
        }
    }
}
