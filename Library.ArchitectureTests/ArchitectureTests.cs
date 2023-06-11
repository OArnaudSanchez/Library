using NetArchTest.Rules;
using Xunit;

namespace Library.ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";

        private const string ApplicationNamespace = "Application";

        private const string InfrastructureNamespace = "Infrastructure";

        private const string ApiNamespace = "API";

        private const string ProjectName = "Library";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(Domain.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                ApiNamespace
            };

            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamespace,
                ApiNamespace,
                DomainNamespace
            };

            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApiNamespace,
                DomainNamespace
            };

            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Api_Should_Not_HaveDependencyOnOtherProjects()
        {
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamespace,
                DomainNamespace
            };

            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAll(otherProjects)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        #region Data Driven Tests Old

        //[Theory]
        //[InlineData($"{ProjectName}.{DomainNamespace}", new[] { ApplicationNamespace, InfrastructureNamespace, ApiNamespace })]
        //[InlineData($"{ProjectName}.{ApplicationNamespace}", new[] { DomainNamespace, InfrastructureNamespace, ApiNamespace })]
        //[InlineData($"{ProjectName}.{InfrastructureNamespace}", new[] { ApiNamespace, DomainNamespace })]
        //[InlineData($"{ProjectName}.{ApiNamespace}", new[] { InfrastructureNamespace, DomainNamespace })]
        //public void Project_Should_Not_HaveDependencyOnOtherProjects(string assemblyName, string[] otherProjects)
        //{
        //    //Arrange
        //    var assembly = Assembly.Load(assemblyName);

        //    //Act
        //    var testResult = Types
        //        .InAssembly(assembly)
        //        .Should()
        //        .NotHaveDependencyOnAll(otherProjects)
        //        .GetResult();

        //    //Assert
        //    Assert.True(testResult.IsSuccessful);
        //}

        #endregion Data Driven Tests Old

        [Fact]
        public void Handlers_Should_HaveDependencyOnDomain()
        {
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Handler.cs")
                .Should()
                .HaveDependencyOn(DomainNamespace)
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnMediatR()
        {
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOn("MediatR")
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Controllers_Should_HaveDependencyOnMediatR()
        {
            var assembly = typeof(API.AssemblyReference).Assembly;

            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller.cs")
                .Should()
                .HaveDependencyOn("MediatR")
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }

        [Fact]
        public void Controllers_Should_Not_HaveDependencyOnDbContext()
        {
            var assembly = typeof(API.AssemblyReference).Assembly;

            var testResult = Types
                .InAssembly(assembly)
                .That()
                .HaveNameEndingWith("Controller.cs")
                .Should()
                .NotHaveDependencyOn("EntityFrameworkCore")
                .GetResult();

            Assert.True(testResult.IsSuccessful);
        }
    }
}