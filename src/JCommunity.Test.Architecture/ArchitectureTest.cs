using FluentAssertions;
using NetArchTest.Rules;

namespace JCommunity.Test.Architecture;

public class ArchitectureTest
{
    private const string AppCoreNS = "JCommunity.AppCore";
    private const string InfrastructureNS = "JCommunity.Infrastructure";
    private const string ServicesNS = "JCommunity.Services";
    private const string WebHostNS = "JCommunity.Web.Host";
 


    [Fact]
    public void AppCore_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(AppCore.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, ServicesNS, WebHostNS,
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
           ServicesNS, WebHostNS
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Services_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Services.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            WebHostNS
        };

        // Act
        var result = Types.InAssembly(assembly)
            .Should()
            .NotHaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }


}

