using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using System.Threading.Tasks;

namespace Questao5.Controllers
{
    [ApiController]
    [Route("api/conta-corrente")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentarContaCommand request)
        {
            var resultado = await _mediator.Send(request);
            return Ok(resultado);
        }

        [HttpGet("saldo/{idContaCorrente}")]
        public async Task<IActionResult> ConsultarSaldo(string idContaCorrente)
        {
            var query = new ObterSaldoQuery { IdContaCorrente = idContaCorrente };
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }
    }
}
