using Microsoft.EntityFrameworkCore;
using SRS.CarRegistration.Abstractions;
using SRS.CarRegistration.Components;
using SRS.CarRegistration.Database;
using SRS.CarRegistration.Repositories;

namespace SRS.CarRegistration;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        string connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("No connectionString");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
        builder.Services.AddScoped<ICarRepository, CarRepository>();

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();


        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseStaticFiles();
        app.UseAntiforgery();

        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
