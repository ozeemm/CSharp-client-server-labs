using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lab2._JWT
{
    public class Program
    {
        static List<Person> people = new()
        {
            new Person("tom@example.com", "12345"),
            new Person("bob@example.com", "55555"),
            new Person("admin", "admin")
        };

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        // Будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,

                        // Строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,

                        // Будет ли валидироваться потребитель токена
                        ValidateAudience = true,

                        // Установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,

                        // Будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // Установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                        // Валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapPost("/login", (Person loginData) =>
            {
                var person = people.FirstOrDefault(p => p.Email == loginData.Email && p.Password == loginData.Password);
                if (person is null)
                    return Results.Unauthorized();

                var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };

                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(AuthOptions.LIFETIME),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    username = person.Email
                };

                return Results.Json(response);
            });

            app.Map("/data", [Authorize] () => new { message = "Тайна в том, что где-то в твоём браузере хранится очень длинный и очень важный ключик!" });

            app.Run();
        }
    }

    record class Person(string Email, string Password);
}
