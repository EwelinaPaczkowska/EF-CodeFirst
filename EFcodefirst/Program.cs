using EFcodefirst.DAL;
using EFcodefirst.Middlewares;
using EFcodefirst.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.OpenApi;


namespace EFcodefirst;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        
        builder.Services.AddDbContext<PrescriptionDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information);
        });
        
        
        builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "PrescriptionApi",
                Description = "api for managing prescriptions",
                Contact = new OpenApiContact
                {
                    Name="Api Support",
                    Email="apiSupport@gmail.com",
                    Url = new Uri("https://github.com/apiSupport")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });
        
        var app = builder.Build();

        app.UseGlobalExceptionHandling();
        
        app.UseSwagger();
        
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "webTripApi");
            
            c.DocExpansion(DocExpansion.List);
            c.DefaultModelExpandDepth(0);
            c.DisplayRequestDuration();
            c.EnableFilter();
            
        });
        

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}