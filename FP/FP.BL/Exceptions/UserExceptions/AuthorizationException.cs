using Microsoft.AspNetCore.Http;

namespace FP.BL.Exceptions.UserExceptions
{
    public class AuthorizationException : Exception, IBaseException
    {
        public int Code => StatusCodes.Status401Unauthorized;

        public string ErrorMessage { get; }
        public AuthorizationException()
        {
            ErrorMessage = "User not logged in";
        }
        public AuthorizationException(string message) : base(message)
        {
            ErrorMessage = message;
        }

        
    }
}
