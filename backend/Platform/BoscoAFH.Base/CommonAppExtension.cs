// using Serilog;
using BoscoAFH.Base.Middlewares; 
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BoscoAFH.Base
{
    public static class CommonAppSetupExtension
    {
        public static IApplicationBuilder UseCommonAppSetup(this IApplicationBuilder app, string swaggerTitle, SwaggerGenOptions swaggerGenOptions)
        {
            // Get the available Swagger documents from the SwaggerGen service

            app.UseSwagger();
            // Serve static files for Swagger UI (CSS, JS)
            app.UseStaticFiles();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                // Swagger JSON endpoints for different parts of your API
                // Get the available Swagger documents from the SwaggerGen service
                // var swaggerGenOptions = app.Services.GetRequiredService<IOptions<SwaggerGenOptions>>().Value;
                // Dynamically add Swagger endpoints based on available Swagger documents
                foreach (var apiDescription in swaggerGenOptions.SwaggerGeneratorOptions.SwaggerDocs)
                {
                    c.SwaggerEndpoint($"/swagger/{apiDescription.Key}/swagger.json", apiDescription.Key);
                }
                // Custom styling and JavaScript
                c.InjectStylesheet("/swagger/swagger-custom.css?t=timestamp");
                c.InjectJavascript("/swagger/swagger-custom-script.js", "text/javascript");

                // Title and other UI configurations
                c.DocumentTitle = swaggerTitle;
                c.DocExpansion(DocExpansion.None); // This will not expand all the API's.
                                                   // Hide the Schemas section at the bottom
                c.DefaultModelExpandDepth(-1);
                c.DisplayRequestDuration();  // Display the request duration in Swagger UI

                // Set the base path for Swagger UI
                c.RoutePrefix = "swagger";

                // Additional configurations
                c.EnableDeepLinking(); // Enable deep linking for operations
            });
            

            // Enable CORS to allow requests from any origin
            // Use CORS
            // app.UseCors("AllowSpecificOrigin");

            app.UseCors("AllowFrontend");

            // Redirect HTTP requests to HTTPS (non-Development only; local SSO uses http://localhost:5001)
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (!env.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            // set the RateLimit to acccess the enpoint per minute 
            //app.UseRateLimiter();

            app.UseAuthentication();

            IConfiguration config = app.ApplicationServices.GetRequiredService<IConfiguration>();

            // Enable authorization (even though authentication is disabled in development)
            app.UseAuthorization();

            return app;
        }
        public static IApplicationBuilder UseCommonAppGatewaySetup(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(); // Handles forwarded headers from the gateway

            app.UseSwagger();

            app.UseCors("AllowSpecificOrigin");
            app.UseCors("AllowFrontend");
            // Serve static files for Swagger UI (CSS, JS)
            app.UseStaticFiles();

            // Redirect HTTP requests to HTTPS
            app.UseHttpsRedirection();

            app.UseResponseCaching();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        public static IApplicationBuilder UseCustomMiddlewareSetup(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseClientInfoMiddleware();

            return app;
        }
    }
}
