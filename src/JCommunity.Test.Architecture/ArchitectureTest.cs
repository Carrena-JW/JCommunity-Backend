using FluentAssertions;
using NetArchTest.Rules;

namespace JCommunity.Test.Architecture;

public class ArchitectureTest
{
    private const string AppCoreNS = "JCommunity.AppCore";
    private const string InfrastructureNS = "JCommunity.Infrastructure";
    private const string ServicesNS = "JCommunity.Services";
    private const string WebHostNS = "JCommunity.Web.Host";
    private const string NotificationHostNS = "JCommunity.Notification.Host";
    private const string IntergrationEventNS = "JCommunity.IntergrationEvent";


    [Fact]
    void AppCore_Project_Check_Dependency_Test()
    {
        // Arrange
        var assembly = typeof(AppCore.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, ServicesNS, WebHostNS, IntergrationEventNS, NotificationHostNS
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
    void Infrastructure_Project_Check_Dependency_Test()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
           ServicesNS, WebHostNS, NotificationHostNS
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
    void Services_Project_Check_Dependency_Test()
    {
        // Arrange
        var assembly = typeof(Services.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            WebHostNS, NotificationHostNS
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
    void IntergrationEvent_Project_Check_Dependency_Test()
    {
        // Arrange
        var assembly = IntergrationEvent.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, ServicesNS, WebHostNS, AppCoreNS, NotificationHostNS
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
    void NotificationHost_Project_Check_Dependency_Test()
    {
        // Arrange
        var assembly = Notification.Host.AssemblyReference.Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, ServicesNS, WebHostNS, AppCoreNS
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

