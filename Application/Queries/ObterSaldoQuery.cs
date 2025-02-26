using MediatR;

namespace Questao5.Application.Queries
{
    public class ObterSaldoQuery : IRequest<SaldoResponse>
    {
        public string IdContaCorrente { get; set; } = string.Empty;
    }
}
