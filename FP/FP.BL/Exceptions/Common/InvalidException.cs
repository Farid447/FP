using FP.Core.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.Common;

public class InvalidException : Exception, IBaseException
{
    public int Code => StatusCodes.Status409Conflict;
    public string ErrorMessage { get; }

    public InvalidException(string message = "Something went wrong!") : base(message)
    {
        ErrorMessage = message;
    }
}