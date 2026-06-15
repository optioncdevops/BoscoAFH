using Microsoft.Extensions.Configuration;

namespace BoscoAFH.Base
{
    public static class ConfigurationExtensions
    {
        public static IConfigurationBuilder AddCustomConfiguration(this IConfigurationBuilder builder)
        {
            // Step 1: Create a temporary config to read "Environment" from appsettings.json
            var tempConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build(); // Build the temporary configuration

            // Step 2: Fetch the environment from appsettings.json (fallback to system variable)
            var env = tempConfig["Environment"] ?? "Production";

            // Step 3: Build the final configuration with the correct environment
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
        }

        public static IConfigurationBuilder AddOcelotConfiguration(this IConfigurationBuilder builder)
        {
            // Step 1: Create a temporary config to read "Environment" from appsettings.json
            var tempConfig = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build(); // Build the temporary configuration

            // Step 2: Fetch the environment from appsettings.json (fallback to system variable)
            var env = tempConfig["Environment"] ?? "Production";

            // Step 3: Build the final configuration with the correct environment
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
