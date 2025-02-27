using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using Questao5.Application.Errors;

namespace Questao5.Application.Commands
{
    /// <summary>
    /// Comando para movimentação de conta corrente (Crédito/Débito)
    /// </summary>
    public class MovimentarContaCommand : IRequest<object>
    {
        /// <summary>
        /// Identificador único para garantir a idempotência da requisição.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CHAVE_IDEMPOTENCIA_OBRIGATORIA)]
        [SwaggerSchema(Description = "Identificador único da requisição para evitar operações duplicadas.")]
        public string ChaveIdempotencia { get; set; } = string.Empty;

        /// <summary>
        /// Identificador da conta corrente na qual será realizada a movimentação.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.CONTA_CORRENTE_OBRIGATORIA)]
        [SwaggerSchema(Description = "ID da conta corrente onde será feita a transação.")]
        public string IdContaCorrente { get; set; } = string.Empty;

        /// <summary>
        /// Valor da transação (Crédito ou Débito). Deve ser positivo.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.VALOR_POSITIVO)]
        [Range(0.01, double.MaxValue, ErrorMessage = ErrorMessages.VALOR_POSITIVO)]
        [SwaggerSchema(Description = "Valor a ser movimentado na conta.")]
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da movimentação: "C" para Crédito ou "D" para Débito.
        /// </summary>
        [Required(ErrorMessage = ErrorMessages.MOVIMENTO_INVALIDO)]
        [RegularExpression("^[CD]$", ErrorMessage = ErrorMessages.MOVIMENTO_INVALIDO)]
        [SwaggerSchema(Description = "Tipo de transação: 'C' para crédito, 'D' para débito.")]
        public string TipoMovimento { get; set; } = string.Empty;
    }
}
