using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Platform.Certificate.API.DAL.Data;
using Platform.Certificate.API.Services;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Common.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddMyDatabase(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<EfContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddMyAuthentication(this IServiceCollection service)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Issuer",
                    ValidAudience = "Audience",
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wwBI_nancf3UoOYQ_jDOoSQRrFL1SR8fbQS-Rsdu1"))
                };
            });
        }

        public static void AddMyCors(this IServiceCollection service, string allowOrigin)
        {
            service.AddCors(o => o.AddPolicy(allowOrigin,
                builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));
        }

        public static void AddControllersConfig(this IServiceCollection service)
        {
            service.AddControllers()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";
                });
        }

        public static void AddMyMapper(this IServiceCollection service)
        {
            service.AddAutoMapper(typeof(Program));
        }

        public static void AddMyValidation(this IServiceCollection service)
        {
            service.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly));
        }

        public static void AddMySwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperCell.Form.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void AddMyScope(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IFileService, FileService>();
            service.AddScoped<ICertificateService, CertificateService>();
            service.AddScoped<IChamberOfCommerceService, ChamberOfCommerceService>();
        }
    }
}