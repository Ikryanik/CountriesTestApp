using Microsoft.Extensions.Configuration;

namespace CountriesTestApp.Common;

public class AppConfig
{
    public ApiConfig ApiConfig { get; }
    public AppConfig(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        configuration.AddEnvironmentVariables();
        ApiConfig = SetApiSection(configuration);
    }
    private static ApiConfig SetApiSection(IConfiguration configuration)
    {
        var apiConfig = new ApiConfig();
        configuration.GetSection(ApiConfig.SectionName).Bind(apiConfig);
        return apiConfig;
    }
}
public class ApiConfig
{
    public const string SectionName = "Api";
    public string Url { get; set; } = string.Empty;
}