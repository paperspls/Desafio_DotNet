using Geo_WebApi_ASP.NET.Data;
using Geo_WebApi_ASP.NET.Security.Implements;
using Geo_WebApi_ASP.NET.Security;
using Geo_WebApi_ASP.NET.Service;
using Geo_WebApi_ASP.NET.Service.Implements;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Geo_WebApi_ASP.NET.Model;
using Geo_WebApi_ASP.NET.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Geo_WebApi_ASP.NET.Configuration;
using Microsoft.OpenApi.Models;

namespace Geo_WebApi_ASP.NET
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // Conexão com o Banco
            if (builder.Configuration["Environment:Start"] == "PROD")
            {
                // Conexão com o PostgresSQL - Nuvem

                builder.Configuration
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("secrets.json");

                var connectionString = builder.Configuration
               .GetConnectionString("ProdConnection");

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(connectionString)
                );
            }
            else
            {
                // Conexão com o SQL Server - Localhost
                var connectionString = builder.Configuration
                .GetConnectionString("DefaultConnection");

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(connectionString)
                );
            }

            // Registrar a Validação das Entidades
            builder.Services.AddTransient<IValidator<Localidade>, LocalidadeValidator>();
            builder.Services.AddTransient<IValidator<User>, UserValidator>();


            // Registrar as Classes de Serviço (Service)
            builder.Services.AddScoped<ILocalidadeService, LocalidadeService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Adicionar a Validação do Token JWT

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var Key = Encoding.UTF8.GetBytes(Settings.Secret);
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //Registrar o Swagger
            builder.Services.AddSwaggerGen(options =>
            {

                //Personalizar a Págna inicial do Swagger
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Projeto GEO IBGE - Balta",
                    Description = "Projeto GEO IBGE - Balta - ASP.NET Core 7 - Entity Framework",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe 32 (Rhyan Magalhães, Julia Alexandrino, Victor Paliari)",
                        Email = "",
                        Url = new Uri("https://github.com/paperspls/Desafio_DotNet")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Github",
                        Url = new Uri("https://github.com/paperspls/Desafio_DotNet")
                    }

                });

                //Adicionar a Segurança no Swagger
                options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Digite um Token JWT válido!",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                //Adicionar a configuração visual da Segurança no Swagger
                options.OperationFilter<AuthResponsesOperationFilter>();

            });

            // Configuração do CORS
            builder.Services.AddCors(options => {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Criar o Banco de dados e as tabelas Automaticamente
            using (var scope = app.Services.CreateAsyncScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();
            }

            app.UseDeveloperExceptionPage();

            // Habilitar o Swagger

            app.UseSwagger();

            app.UseSwaggerUI();


            // Swagger Como Página Inicial - Nuvem
            if (app.Environment.IsProduction())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto Balta - v1");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}