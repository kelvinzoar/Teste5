namespace Questao5.Models
{
	public class SaldoResponse
	{
		public int NumeroConta { get; set; }
		public string NomeTitular { get; set; } = string.Empty;
		public DateTime DataConsulta { get; set; }
		public decimal Saldo { get; set; }
	}
}
