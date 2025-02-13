using FP.BL.Extentions;
using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.FileExceptions;
public class TypeorSizeValidException : Exception, IBaseException
{
    public int Code => StatusCodes.Status400BadRequest;

    public string ErrorMessage { get; }

    public TypeorSizeValidException()
    {
        ErrorMessage = $"File type must be {FileExtention.Type}, File size must be less than {FileExtention.Size} mb";
    }

    public TypeorSizeValidException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
