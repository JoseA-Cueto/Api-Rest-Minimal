using System;

namespace ApiRestMinimal.Exceptions.Base
{
    public abstract class ExceptionBase : Exception
    {
        public int StatusCode { get; }

        protected ExceptionBase(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
