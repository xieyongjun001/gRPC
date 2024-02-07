using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.gRPC.Services;
using Microsoft.EntityFrameworkCore;

namespace CareerCloud.gRPC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

        // Add services to the container.
        builder.Services.AddGrpc();

        //Add the dbContext
        var connectionString = builder.Configuration.GetConnectionString("CareerCloudConString");
        builder.Services.AddDbContext<CareerCloudContext>(options =>
        {
            options.UseSqlServer(connectionString!);
        });

        var app = builder.Build();
        app.MapGrpcService<ApplicantEducationService>();
        app.MapGrpcService<ApplicantJobApplicationService>();
        app.MapGrpcService<ApplicantProfileService>();
        app.MapGrpcService<CompanyDescriptionService>();
        app.MapGrpcService<CompanyJobService>();
        app.MapGrpcService<CompanyJobEducationService>();
        app.MapGrpcService<SecurityLoginService>();
        app.MapGrpcService<SecurityLoginsLogService>();
        app.MapGrpcService<SystemLanguageCodeService>();

        // Configure the HTTP request pipeline.
        //app.MapGrpcService<GreeterService>();
        app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

        app.Run();
    }
}
