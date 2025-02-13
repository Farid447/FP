using FP.Core.Entities.Common;
using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.Common;

public class NotFoundException<T> : Exception, IBaseException
    where T : BaseEntity, new()
{
    public int Code => StatusCodes.Status404NotFound;
    public string ErrorMessage { get; }
    const string _message = " not found";

    public NotFoundException():base(typeof(T).Name + _message)
    {
        ErrorMessage = typeof(T).Name+ _message;
    }
    public NotFoundException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}