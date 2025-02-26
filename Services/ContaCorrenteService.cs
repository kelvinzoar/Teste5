using Questao5.Models;
using Questao5.Repositories;
using System;
using System.Threading.Tasks;

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
                return new ErrorResponse { TipoErro = "INVALID_ACCOUNT", Mensagem = "Conta não encontrada." };

            if (!await _repository.ContaAtiva(request.IdContaCorrente))
                return new ErrorResponse { TipoErro = "INACTIVE_ACCOUNT", Mensagem = "Conta inativa." };

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
