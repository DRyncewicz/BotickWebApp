using BotickAPI.Application;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Filters;
using BotickAPI.Infrastructure;
using BotickAPI.Infrastructure.Identity;
using BotickAPI.Persistence;
using BotickAPI.Persistence.Context;
using BotickAPI.Service;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using HealthChecks.UI.Client;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
builder.Services.AddCors(options =>
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7048", "https://localhost:5001")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
if (builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
                       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
    builder.Services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddIdentityServer()
        .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
        {

            options.ApiResources.Add(new ApiResource("api1"));
            options.ApiScopes.Add(new ApiScope("api1", "scope"));
            options.Clients.Add(new Client
            {
                ClientId = "client",
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "openid", "profile", "Botick.ApiAPI,", "api1" }
            });
        })
        .AddTestUsers(new List<TestUser> {
            new TestUser
        {
                SubjectId = "f624a6bd-fdee-474d-a711-4d01905f606d",
                Username = "alice",
                Password = "Pass123$",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Email, "alice@user.com"),
                    new Claim(ClaimTypes.Name, "alice"),
                    new Claim(ClaimTypes.Role, "Organiser")
                }
        }});
    builder.Services.AddAuthentication("Bearer")
        .AddIdentityServerJwt();
}
else
{
    builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = false,
        };
    });
}
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api1");
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows()
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                {
                    {"api1", "Full access"},
                    {"user", "User info"},
                    {"openid", "user info"}
                }
            }
        }
    });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Botick v1");
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("secret");
        c.OAuthUsePkce();
        c.OAuth2RedirectUrl("https://localhost:7086/swagger/oauth2-redirect.html");

    });
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

app.UseAuthentication();
if (app.Environment.IsEnvironment("Test"))
{
    app.UseIdentityServer();
}

app.UseRouting();

app.UseCors("MyAllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers().RequireAuthorization("ApiScope");

app.Run();

public partial class Program { }

