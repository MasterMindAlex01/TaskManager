using System.Net;

namespace TaskManager.Application.Common.Exceptions;

public class UnauthorizedException : CustomException
{
    public UnauthorizedException(string message)
       : base(message, null, HttpStatusCode.Unauthorized)
    {
    }
}