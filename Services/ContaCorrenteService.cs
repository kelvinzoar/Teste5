using Questao5.Models;
using Questao5.Repositories;
using System;
using System.Threading.Tasks;
using Questao5.Application.Errors;

namespace Questao5.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private readonly IContaCorrenteRepository _repository;

        public ContaCorrenteService(IContaCorrenteRepository repository)
        {
            _repository = repository;
        }

        public async Task<object> MovimentarConta(MovimentacaoRequest request)
        {
            if (!await _repository.ContaExiste(request.IdContaCorrente))
                return new ErrorResponse("Conta não encontrada.", ErrorCodes.INVALID_ACCOUNT);

            if (!await _repository.ContaAtiva(request.IdContaCorrente))
                return new ErrorResponse("Conta inativa.", ErrorCodes.INACTIVE_ACCOUNT);

            await _repository.RegistrarMovimento(request);
            return new { Sucesso = true };
        }

        public async Task<SaldoResponse> ObterSaldo(string idContaCorrente)
        {
            var dados = await _repository.ObterDadosConta(idContaCorrente);
            var saldo = await _repository.ObterSaldo(idContaCorrente);

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
