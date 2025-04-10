using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Lab1._WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(); 
            builder.Services.AddOpenApi();

            var connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Init DB
            using (var scope = app.Services.CreateScope()) 
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.EnsureCreated();
                dbContext.SeedData();
            }

            app.MapControllers();

            app.MapGet("/", () =>
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html");
                return Results.File(path, "text/html");
            });

            app.Run();
        }
    }
}
