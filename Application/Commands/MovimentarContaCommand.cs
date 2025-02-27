using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Questao5.Application.Errors;

namespace Questao5.Application.Commands
{
    /// <summary>
    /// Comando para movimenta��o de conta corrente (Cr�dito/D�bito)
    /// </summary>
    public class MovimentarContaCommand : IRequest<object>
    {
        /// <summary>
        /// Identificador �nico para garantir a idempot�ncia da requisi��o.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CHAVE_IDEMPOTENCIA_OBRIGATORIA)]
        [SwaggerSchema(Description = "Identificador �nico da requisi��o para evitar opera��es duplicadas.")]
        public string ChaveIdempotencia { get; set; } = string.Empty;

        /// <summary>
        /// Identificador da conta corrente na qual ser� realizada a movimenta��o.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CONTA_CORRENTE_OBRIGATORIA)]
        [SwaggerSchema(Description = "ID da conta corrente onde ser� feita a transa��o.")]
        public string IdContaCorrente { get; set; } = string.Empty;

        /// <summary>
        /// Valor da transa��o (Cr�dito ou D�bito). Deve ser positivo.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.VALOR_POSITIVO)]
        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.VALOR_POSITIVO)]
        [SwaggerSchema(Description = "Valor a ser movimentado na conta.")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da movimenta��o: "C" para Cr�dito ou "D" para D�bito.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.MOVIMENTO_INVALIDO)]
        [RegularExpression("^[CD]$", ErrorMessage = ErrorMessages.MOVIMENTO_INVALIDO)]
        [SwaggerSchema(Description = "Tipo de transa��o: 'C' para cr�dito, 'D' para d�bito.")]
        public string TipoMovimento { get; set; } = string.Empty;
    }
}
