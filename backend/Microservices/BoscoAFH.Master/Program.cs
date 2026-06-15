using BoscoAFH.Base.Extension;
using BoscoAFH.Master;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Resources;

[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]
var builder = WebApplication.CreateBuilder(args).UseSecureKestrel();

// Load configuration using the helper
builder.Configuration.AddConfiguration(ConfigurationLoader.LoadConfiguration());

// builder.Services.AddDbContext<OptionCDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
});

builder.Services.AddCommonServicesSetup();

builder.Services.AddDIServicesSetup();

// JWT Authentication
builder.Services.AddAuthenticationSetup(builder.Configuration);

builder.Services.AddAutoMappingSetup();
// Go to Project Properties --> Build --> Output  --> XML Documentation File --> Check the checkbox
// Error and Warning  --> Suppress specific warning ass the ;1591 and
// Output             --> Check the Documentation file
// Set the comments path for the Swagger JSON and UI.
// To register the swagger generator
// Add SwaggerGen with XML comments
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true); // Important!
});

// To register the swagger generator
builder.Services.AddSwaggerGenSetup(SwaggerModuleDoc.BoscoAFHMasterDocs);

// Allow the Local system to access the api with jwt token
builder.Services.DisableAuthenticationPolicy(builder.Environment);

// Use empty fallback to satisfy nullable flow; extension handles invalid values.
//var accessHubAuditConnection = builder.Configuration.GetConnectionString("AuditLogDB") ?? string.Empty;
//builder.Host.AddSerilogConfiguration(accessHubAuditConnection, AuditTableName.AccessHub);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCommonAppSetup(SwaggerModuleDoc.BoscoAFHMasterDocs, app.Services.GetRequiredService<IOptions<SwaggerGenOptions>>().Value);

app.UseCustomMiddlewareSetup();

app.MapControllers();

// Scalar reads the same Swashbuckle-generated OpenAPI JSON as Swagger UI (/swagger/{doc}/swagger.json).
app.MapScalarForSwashbuckle(SwaggerModuleDoc.BoscoAFHMasterDocs, SwaggerModuleDoc.BoscoAFHMaster);

app.MapGet("/", () => Results.Text(DefaultData.WebStartPage.Replace("{0}", SwaggerModuleDoc.BoscoAFHMaster), "text/html")).ExcludeFromDescription(); // Exclude this endpoint from Swagger

app.Run();
