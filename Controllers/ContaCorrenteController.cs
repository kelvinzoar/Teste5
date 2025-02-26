using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Questao5.Application.Errors;
using System;
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

        /// <summary>
        /// Movimentar Conta (Crédito ou Débito)
        /// </summary>
        [HttpPost("movimentar")]
        public async Task<IActionResult> MovimentarConta([FromBody] MovimentarContaCommand request)
        {
            try
            {
                var resultado = await _mediator.Send(request);
                return Ok(resultado);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ex.ErrorCode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("Erro interno ao processar a transação", ErrorCodes.INTERNAL_ERROR));
            }
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente
        /// </summary>
        [HttpGet("saldo/{idContaCorrente}")]
        public async Task<IActionResult> ConsultarSaldo(string idContaCorrente)
        {
            try
            {
                var query = new ObterSaldoQuery { IdContaCorrente = idContaCorrente };
                var resultado = await _mediator.Send(query);

                return Ok(resultado);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ex.ErrorCode));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse("Erro interno ao consultar saldo", ErrorCodes.INTERNAL_ERROR));
            }
        }
    }
}
