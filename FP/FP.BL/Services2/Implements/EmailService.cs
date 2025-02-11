using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using BlogApp.BL.DTOs.Options;
using BlogApp.BL.Exceptions.Common;
using BlogApp.BL.ExternalServices.Interfaces;
using BlogApp.BL.Services.Interfaces;
using BlogApp.Core.Entities;
using BlogApp.Core.Enums;
using BlogApp.Core.Repositories;
using BlogApp.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BlogApp.BL.Services.Implements;

public class EmailService:IEmailService
{
    readonly SmtpClient _client;
    readonly MailAddress _from;
    readonly IUserRepository _repo;
    readonly ICurrentUser _user;
    readonly IMemoryCache _cache;
    readonly IHttpContextAccessor _httpContextAccessor;
    public EmailService(IOptions<EmailOptions> options,
        IMemoryCache cache,
        IHttpContextAccessor httpContextAccessor,
        IUserRepository repo,
        ICurrentUser user)
    {
        var opt = options.Value;
        _client = new(opt.Host, opt.Port);
        _client.Credentials = new NetworkCredential(opt.Sender, opt.Password);
        _client.EnableSsl = true;
        _from = new MailAddress(opt.Sender,"Elmin");
        _cache = cache;
        _httpContextAccessor = httpContextAccessor;
        _repo = repo;
        _user = user;
    }
    public async Task SendEmailConfirmation()
    {
        string? email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        string? name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }
       
        var token = Guid.NewGuid().ToString();
        _cache.Set(name, token, TimeSpan.FromMinutes(10));
        MailAddress to = new(email);
        MailMessage msg = new(_from, to);
        msg.Subject = "Confirm your email address";
        msg.Body = token;
        await _client.SendMailAsync(msg);
    }

    public async Task AccountVerify(string userToken)
    {
        string? name = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        //var cacheToken = _cache.Get<string>(name);
        //if (string.IsNullOrEmpty(userToken) || string.IsNullOrEmpty(name))
        //{
        //    throw new UnauthorizedAccessException("User is not authenticated or token is missing.");
        //}
        //if (!(cacheToken != null && cacheToken == userToken))
        //    throw new NotFoundException<User>();
        
        User? user = await _repo.GetByIdAsync(_user.GetId());
        user!.Role = user!.Role | (int)Roles.Publisher;
        await _repo.SaveAsync();
    }

}