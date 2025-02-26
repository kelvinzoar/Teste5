using Questao5.Models;

namespace Questao5.Models
{
	public class SaldoResponse
	{
		public int NumeroConta { get; set; }
		public string NomeTitular { get; set; } = string.Empty;
		public DateTime DataConsulta { get; set; } = DateTime.UtcNow;
		public decimal Saldo { get; set; }
	}
}
