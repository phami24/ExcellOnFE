using Xunit;
namespace Architecture.Test
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamespace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string WebNamespace = "API";
        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrage
            var assembly = typeof(Domain.SomeClassInDomain).Assembly;
            // Act

            // Assert
        }
    }
}