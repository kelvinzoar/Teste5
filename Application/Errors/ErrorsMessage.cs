namespace Questao5.Application.Errors
{
    /// <summary>
    /// Classe contendo mensagens de erro reutilizáveis.
    /// </summary>
    public static class ErrorMessages
    {
        public const string CHAVE_IDEMPOTENCIA_OBRIGATORIA = "A chave de idempotência é obrigatória.";
        public const string CONTA_CORRENTE_OBRIGATORIA = "O ID da conta corrente é obrigatório.";
        public const string VALOR_POSITIVO = "O valor deve ser positivo.";
        public const string MOVIMENTO_INVALIDO = "Tipo inválido. Deve ser 'C' (Crédito) ou 'D' (Débito).";
    }
}
