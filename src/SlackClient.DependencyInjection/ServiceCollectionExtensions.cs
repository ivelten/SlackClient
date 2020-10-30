using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace SlackClient.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureSlackClient(this IServiceCollection services, IConfiguration configuration) =>
            services.AddScoped<ISlackClient>(serviceProvider => new SlackClient(configuration.GetSection("Slack").GetValue<string>("UrlAccessWithToken")));
    }
}
