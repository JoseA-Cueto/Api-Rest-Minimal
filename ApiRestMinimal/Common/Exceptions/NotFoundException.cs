using ApiRestMinimal.Common.Exceptions.Base;

namespace ApiRestMinimal.Common.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message, 404)
    {
    }
}