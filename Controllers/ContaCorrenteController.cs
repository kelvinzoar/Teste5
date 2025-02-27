using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Questao5.Application.Errors;
using System.Threading.Tasks;
using Questao5.Models;

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
        /// <param name="request">Dados da movimentação</param>
        /// <returns>Confirmação da transação</returns>
        [HttpPost("movimentar")]
        [SwaggerOperation(Summary = "Realiza um crédito ou débito em uma conta corrente",
                          Description = "Essa operação movimenta o saldo da conta corrente com um crédito ('C') ou débito ('D').")]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
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
            catch
            {
                return StatusCode(500, new ErrorResponse("Erro interno ao processar a transação", ErrorCodes.INTERNAL_ERROR));
            }
        }

        /// <summary>
        /// Consulta o saldo de uma conta corrente
        /// </summary>
        /// <param name="idContaCorrente">ID da conta corrente</param>
        /// <returns>Detalhes do saldo da conta</returns>
        [HttpGet("saldo/{idContaCorrente}")]
        [SwaggerOperation(Summary = "Consulta o saldo de uma conta corrente",
                          Description = "Retorna o saldo disponível de uma conta corrente.")]
        [ProducesResponseType(typeof(SaldoResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        public async Task<ActionResult<SaldoResponse>> ConsultarSaldo(string idContaCorrente)
        {
            try
            {
                var query = new ObterSaldoQuery { IdContaCorrente = idContaCorrente };
                var resultado = await _mediator.Send(query);

                if (resultado == null)
                {
                    return BadRequest(new ErrorResponse("Conta não encontrada", ErrorCodes.INVALID_ACCOUNT));
                }

                return Ok(resultado);
            }
            catch (BusinessException ex)
            {
                return BadRequest(new ErrorResponse(ex.Message, ex.ErrorCode));
            }
            catch
            {
                return StatusCode(500, new ErrorResponse("Erro interno ao consultar saldo", ErrorCodes.INTERNAL_ERROR));
            }
        }
    }
}
