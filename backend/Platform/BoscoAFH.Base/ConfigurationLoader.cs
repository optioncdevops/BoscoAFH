using Microsoft.Extensions.Configuration;

namespace BoscoAFH.Base
{
    public static class ConfigurationLoader
    {
        public static IConfiguration LoadConfiguration()
        {
            //var env = builder.Configuration["Environment"];

            //// Load environment-specific configuration
            //builder.Configuration
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            //    .AddEnvironmentVariables();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var tempConfig = builder.Build();
            var env = tempConfig["Environment"]; // Read environment after initial load

            builder.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
