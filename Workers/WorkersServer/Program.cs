using Microsoft.EntityFrameworkCore;
using WorkersServer.Data;
using WorkersServer.Models.MappingProfiles;
using WorkersServer.Services;

namespace WorkersServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddGrpc();
            builder.Services.AddAutoMapper(typeof(WorkerProfile));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                InitialSeeder.Initialize(services);
            }

            // Configure the HTTP request pipeline.
            app.MapGrpcService<WorkerIntegrationService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}