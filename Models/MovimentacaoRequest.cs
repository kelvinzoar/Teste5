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
        [RegularExpression("^[CD]$", ErrorMessage = "Tipo inv�lido. Deve ser 'C' (Cr�dito) ou 'D' (D�bito).")]
        public string TipoMovimento { get; set; } = string.Empty;
    }
}
