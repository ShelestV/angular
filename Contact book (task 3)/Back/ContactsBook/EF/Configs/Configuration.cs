using Microsoft.Extensions.Configuration;

namespace EF.Configs
{
    public static class Configuration
    {
        public static IConfiguration Config { get; private set; }

        public static bool IsConfigure => Config is not null;
        
        public static void Configure(string[] args)
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        public static void Configure(IConfiguration configuration)
        {
            Config = configuration;
        }
    }
}