using BotickAPI.Application;
using BotickAPI.Infrastructure;
using BotickAPI.Persistence;
using BotickAPI.Persistence.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:44364");
        }));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "BotickApi",
        Version = "v1",
        Description = "Will be updated",
        TermsOfService = new Uri("http://example/termsofuse.pl"),
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT Micense",
            Url = new Uri("https://choosealicense.com/licenses/mit/")
        },
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Dominik",
            Email = "dominik.ryncewicz@gmail.com",
            Url = new Uri("https://github.com/DRyncewicz")
        }
    });
    var filePath = Path.Combine(AppContext.BaseDirectory, "BotickApi.xml");
    c.IncludeXmlComments(filePath);
});
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.ReadFrom.Configuration(hostContext.Configuration);
});
builder.Services.AddHealthChecks()
    .AddDbContextCheck<BotickDbContext>();
builder.Services.AddHealthChecksUI(opt =>
{
    opt.SetEvaluationTimeInSeconds(15);
    opt.MaximumHistoryEntriesPerEndpoint(60);
    opt.SetApiMaxActiveRequests(1);

    opt.AddHealthCheckEndpoint("default api", "/hc");
}).AddInMemoryStorage();
try
{
    Log.Information("Application is starting up");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Could not start up application");
}
finally
{
    Log.CloseAndFlush();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecksUI();

app.UseHealthChecksUI(config => config.UIPath = "/hc-ui");

app.UseHealthChecks("/hc", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
