using Questao5.Models;
using System.Threading.Tasks;

namespace Questao5.Services
{
    public interface IContaCorrenteService
    {
        Task<object> MovimentarConta(MovimentacaoRequest request);
        Task<SaldoResponse> ObterSaldo(string idContaCorrente);
    }
}
