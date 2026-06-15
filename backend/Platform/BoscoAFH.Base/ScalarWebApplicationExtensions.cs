using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace BoscoAFH.Base;

public static class ScalarWebApplicationExtensions
{
    /// <summary>
    /// Serves Scalar at <c>/scalar</c> using the same OpenAPI documents as Swashbuckle
    /// (<c>/swagger/{documentName}/swagger.json</c>).
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <param name="swaggerModuleDocsCsv">Comma-separated Swashbuckle document names (same string as <see cref="ServiceExtension.AddSwaggerGenSetup"/>).</param>
    /// <param name="apiReferenceTitle">Title shown in the Scalar UI.</param>
    public static WebApplication MapScalarForSwashbuckle(
        this WebApplication app,
        string swaggerModuleDocsCsv,
        string apiReferenceTitle)
    {
        var documentNames = swaggerModuleDocsCsv
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        app.MapScalarApiReference("/scalar", options =>
        {
            options
                .WithTitle(apiReferenceTitle)
                .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json")
                .AddDocuments(documentNames);
        });

        return app;
    }
}
