using System.ComponentModel.DataAnnotations;

namespace Questao5.Models
{
    public class MovimentacaoRequest
    {
        [Required]
        public string ChaveIdempotencia { get; set; } = string.Empty;

        [Required]
        public string IdContaCorrente { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Valor { get; set; }

        [Required]
        [RegularExpression("^[CD]$", ErrorMessage = "Tipo inválido. Deve ser 'C' (Crédito) ou 'D' (Débito).")]
        public string TipoMovimento { get; set; } = string.Empty;
    }
}
