namespace Questao5.Application.Errors
{
    public class ErrorResponse
    {
        public string Mensagem { get; set; }
        public string Tipo { get; set; }

        public ErrorResponse(string mensagem, string tipo)
        {
            Mensagem = mensagem;
            Tipo = tipo;
        }
    }
}
