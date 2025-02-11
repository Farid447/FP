namespace BlogApp.BL.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailConfirmation();
    Task AccountVerify(string userToken);
}