using System;

namespace Questao5.Application.Errors
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }

        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
