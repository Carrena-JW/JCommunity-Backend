using FluentAssertions;
using JCommunity.AppCore.Core.Abstractions;
using JCommunity.AppCore.Core.Errors;
using JCommunity.AppCore.Entities.MemberAggregate;
using JCommunity.Services.MemberService.Commands;
using Microsoft.Extensions.Logging;
using Moq;

namespace JCommunity.Test.Service.Handlers;

public class CreateMemberTest
{
	CreateMember.Command.Handler _handler;
	Mock<IMemberRepository> _repository;
	Mock<ILogger<CreateMember.Command.Handler>> _logger;

    public CreateMemberTest()
	{ 		
		_repository = new Mock<IMemberRepository>();
		_logger = new Mock<ILogger<CreateMember.Command.Handler>>();
		_handler = new CreateMember.Command.Handler(_repository.Object, _logger.Object);
	}

    // Base Command 
    CreateMember.Command command = new()
    {
        Name = "Ji Woong",
        NickName = "102boy",
        Email = "102boy@gmail.com",
        Password = "Pa$$w0rd1234"
    };

    void Setup_IsUniqueEmailAsync(bool result)
    {
        _repository.Setup(x =>
           x.IsUniqueEmailAsync(It.IsAny<string>(), new ())
       ).ReturnsAsync(result);
    }

    void Setup_IsUniqueNickNameAsync(bool result)
    {
        _repository.Setup(x =>
           x.IsUniqueNickNameAsync(It.IsAny<string>(), new ())
       ).ReturnsAsync(result);
    }

    [Fact]
    async void CreateMember_Test()
    {
        // Arrange
        Setup_IsUniqueEmailAsync(true);
        Setup_IsUniqueNickNameAsync(true);

        var unitOfWorkMock = new Mock<IUnitOfWork>(); 
        unitOfWorkMock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(Task.FromResult(1));

        _repository.Setup(repo => repo.UnitOfWork).Returns(unitOfWorkMock.Object);

        var member = Member.Create(command.Name, command.NickName, command.Password, command.Email);

        _repository.Setup(x =>
            x.Add(It.IsAny<Member>())
        ).Returns(member);

        // Act
        var result = await _handler.Handle(command, new());

        // Assert
        result.Value.Should().BeOfType(typeof(string));
        Ulid.TryParse(result.Value,out _).Should().BeTrue();
    }

    [Fact]
	async void CreateMember_Not_Unique_Email_Error_Test()
	{

        // Arrange
        Setup_IsUniqueEmailAsync(false);

        // Act
		var result = await _handler.Handle(command,new ());

        // Assert
        result.Errors.Should().Contain(x => x.GetType().Equals(typeof(MemberError.EmailNotUnique)));
	}

    [Fact]
    async void CreateMember_Not_Unique_Nickname_Error_Test()
    {
        // Arrange
        // pass unique email
        Setup_IsUniqueEmailAsync(true);
        Setup_IsUniqueNickNameAsync(false);

        // Act
        var result = await _handler.Handle(command, new ());

        // Assert
        result.Errors.Should().Contain(x => x.GetType().Equals(typeof(MemberError.NicknameNotUnique)));
    }
}

