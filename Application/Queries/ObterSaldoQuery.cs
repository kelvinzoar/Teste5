using MediatR;
using Questao5.Models;

namespace Questao5.Application.Queries
{
    public class ObterSaldoQuery : IRequest<SaldoResponse>
    {
        public string IdContaCorrente { get; set; } = string.Empty;
    }
}
