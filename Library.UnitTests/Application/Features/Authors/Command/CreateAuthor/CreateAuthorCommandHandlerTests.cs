using AutoMapper;
using Library.Application.DTOs;
using Library.Application.Features.Authors.Command.CreateAuthor;
using Library.Application.Interfaces.Persistence;
using Library.Application.Mappings;
using Library.Domain.Entities;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Library.UnitTests.Application.Features.Authors.Command.CreateAuthor
{
    public class CreateAuthorCommandHandlerTests
    {
        private readonly Mock<IAuthorRepository> _authorRepositoryMock;

        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        private readonly IMapper _mapper;

        private readonly CreateAuthorCommandHandler _sut;

        public CreateAuthorCommandHandlerTests()
        {
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            var configuration = new MapperConfiguration(config => config.AddProfile(new AuthorMappingConfiguration()));
            _mapper = new Mapper(configuration);
            _sut = new CreateAuthorCommandHandler(_authorRepositoryMock.Object, _unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task Handler_Should_CreateAnAuthorAndReturnAuthorDto()
        {
            //Arrange
            var request = new CreateAuthorCommand
            {
                Name = "Test",
                LastName = "Test",
                Email = "Test@gmail.com",
                BirthDate = DateTime.Now,
                Gender = Domain.Enums.Gender.Male
            };

            //Act
            var result = await _sut.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            _authorRepositoryMock.Verify(x => x.AddAuthorAsync(It.IsAny<Author>(), It.IsAny<CancellationToken>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<AuthorDto>(result);
        }
    }
}