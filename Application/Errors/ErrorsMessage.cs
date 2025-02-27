namespace Questao5.Application.Errors
{
    /// <summary>
    /// Classe contendo mensagens de erro reutiliz�veis.
    /// </summary>
    public static class ErrorMessages
    {
        public const string CHAVE_IDEMPOTENCIA_OBRIGATORIA = "A chave de idempot�ncia � obrigat�ria.";
        public const string CONTA_CORRENTE_OBRIGATORIA = "O ID da conta corrente � obrigat�rio.";
        public const string VALOR_POSITIVO = "O valor deve ser positivo.";
        public const string MOVIMENTO_INVALIDO = "Tipo inv�lido. Deve ser 'C' (Cr�dito) ou 'D' (D�bito).";
    }
}
