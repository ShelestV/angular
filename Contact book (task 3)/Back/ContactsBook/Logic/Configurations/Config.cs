using Microsoft.Extensions.Configuration;

namespace Logic.Configurations
{
    public static class Config
    {
        public static void Configure(IConfiguration configuration)
        {
            EF.Configs.Configuration.Configure(configuration);
        }

        public static void Configure(string[] args)
        {
            EF.Configs.Configuration.Configure(args);
        }
    }
}