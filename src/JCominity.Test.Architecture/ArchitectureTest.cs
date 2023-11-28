using NetArchTest.Rules;
using FluentAssertions;


namespace JComunity.Test.Architecture;

public class ArchitectureTest
{
    private const string CommonNS = "JComunity.Core";
    private const string DomainNS = "JComunity.Domain";
    private const string ApplicationNS = "JComunity.Application";
    private const string InfrastructureNS = "JComunity.Infrastructure";
    private const string PresentationNS = "JComunity.Presentation";
    private const string WebHostNS = "JComunity.Host.Web";


    [Fact]
    public void 공통_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Core.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            DomainNS, ApplicationNS, InfrastructureNS, PresentationNS, WebHostNS,
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
    public void 도메인_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Domain.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            CommonNS, ApplicationNS, InfrastructureNS, PresentationNS, WebHostNS,
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
    public void 어플리케이션_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Application.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            PresentationNS, WebHostNS, InfrastructureNS
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
    public void 인프라스트럭처_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            PresentationNS, WebHostNS
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
    public void 프리젠테이션_프로젝트_종속성_확인()
    {
        // Arrange
        var assembly = typeof(Presentation.AssemblyReference).Assembly;
        var otherProjects = new[]
        {
            InfrastructureNS, WebHostNS
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

