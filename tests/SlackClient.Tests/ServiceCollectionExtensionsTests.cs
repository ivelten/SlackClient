using System.IO;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlackClient.DependencyInjection;
using FluentAssertions;

namespace SlackClient.Tests
{
    public class Tests
    {
        private ServiceProvider _serviceProvider;

        [SetUp]
        public void Setup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            _serviceProvider = new ServiceCollection()
                 .ConfigureSlackClient(configuration)
                 .BuildServiceProvider();
        }

        [Test]
        public void ClientMustBeOfExpectedType()
        {
            _serviceProvider.GetRequiredService<ISlackClient>().Should().BeOfType<SlackClient>();
        }
    }
}