using FP.BL.Extensions;
using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.FileExceptions;
public class TypeorSizeValidException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public TypeorSizeValidException()
    {
        ErrorMessage = $"File type must be {FileExtension.Type}, File size must be less than {FileExtension.Size} mb";
    }

    public TypeorSizeValidException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
