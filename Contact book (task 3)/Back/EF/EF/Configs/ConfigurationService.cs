using Microsoft.Extensions.Configuration;

namespace EF.Configs
{
    internal static class ConfigurationService
    {
        public static IConfiguration Configuration { get; private set; }
        public static bool IsConfigured => Configuration is not null;

        public static void Configure(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("Configs.appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        /// <summary>
        /// For migrations
        /// </summary>
        public static void Configure()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}