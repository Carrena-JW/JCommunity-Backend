using NetArchTest.Rules;
using FluentAssertions;
using JComunity.AppCore;

namespace JComunity.Test.Architecture;

public class ArchitectureTest
{
    private const string AppCoreNS = "JComunity.AppCore";
    private const string InfrastructureNS = "JComunity.Infrastructure";
    private const string ServicesNS = "JComunity.Services";
    private const string WebHostNS = "JComunity.Web.Host";
    private const string WebContract = "JComunity.Web.Contract";


    [Fact]
    public void AppCore_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(AppCore.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, ServicesNS, WebContract, WebHostNS,
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
           ServicesNS, WebContract, WebHostNS
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
            WebContract, WebHostNS
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
    public void Web_Contract_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Web.Contract.AssemblyReference).Assembly;
        var otherProjects = new[]
        { 
            AppCoreNS, ServicesNS, InfrastructureNS, WebHostNS
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

