using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.UserExceptions;
public class UserHasNoPermissionException : Exception, IBaseException
{
    public int Code => StatusCodes.Status403Forbidden;

    public string ErrorMessage { get; }
    public UserHasNoPermissionException()
    {
        ErrorMessage = "User has no permission";
    }

    public UserHasNoPermissionException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
