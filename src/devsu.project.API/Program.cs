using devsu.project.API;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
//services
builder.Services
    .AddServices(builder.Configuration);

//serilog
var logger = new LoggerConfiguration()
  .MinimumLevel.Error()
  .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("appConnectionString"),
                       tableName: "Logs")
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Configure the HTTP request pipeline.

var app = builder.Build()
    .AddPipeline();

app.Run();

public partial class Program { };