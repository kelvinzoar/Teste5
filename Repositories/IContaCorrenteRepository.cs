using Questao5.Models;
using System.Threading.Tasks;

namespace Questao5.Repositories
{
    public interface IContaCorrenteRepository
    {
        Task<bool> ContaExiste(string idContaCorrente);
        Task<bool> ContaAtiva(string idContaCorrente);
        Task RegistrarMovimento(MovimentacaoRequest request);
        Task<decimal> ObterSaldo(string idContaCorrente);
        Task<(int Numero, string Nome)> ObterDadosConta(string idContaCorrente);
    }
}
