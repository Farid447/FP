using FP.BL;
using FP.Core.Entities;
using FP.DAL;
using FP.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace FP;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddDbContext<FPDbContext>(opt=>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
        });

        builder.Services.AddAuth(builder.Configuration);
        builder.Services.AddJwtOptions(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddHttpContextAccessor();


        builder.Services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.SignIn.RequireConfirmedEmail = false;
            opt.Password.RequiredLength = 8;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;
            opt.Lockout.MaxFailedAccessAttempts = 10;
            opt.Lockout.DefaultLockoutTimeSpan = new TimeSpan(30000);
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<FPDbContext>();

        builder.Services.AddAutoMapper(typeof(Program));

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.AddExceptionHandler();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
