namespace ApiRestMinimal.Common.Exceptions.Base;

public abstract class ExceptionBase : Exception
{
    protected ExceptionBase(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; }
}