using JComunity.AppCore.Entities.MemberAggregate;
using JComunity.Web.Contract.MemberApi.Validators;
using Moq;

namespace JComunity.Test.Service.EndpointTest;

public class MemberApiValidatorTest
{
    IValidator<CreateMemberRequest> _createMemberValidator;
    Mock<IMemberRepository> _memberRepository;

    public MemberApiValidatorTest()
    {
        _memberRepository = new Mock<IMemberRepository>();
        _createMemberValidator = new CreateMemberRequestValidator(_memberRepository.Object);
    }

   


    [Fact]
    async void CreateMemberRequestTest_CASE_01()
    {
        // Setup
        _memberRepository.Setup(x =>
            x.IsUniqueEmail(It.IsAny<string>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        _memberRepository.Setup(x =>
            x.IsUniqueNickName(It.IsAny<string>(), It.IsAny<CancellationToken>())
        ).ReturnsAsync(true);

        // Arrange
        var request = new CreateMemberRequest(
            "Michael Jordan",
            "MJ",
            "MJ@Test.com",
            "Pa$$w0rd1234");

        var request_2 = request with { name = "Michael Jordanddddddddddddddddddddddddddddddddddddd" };

        var request_3 = request with { name = "M" };

        var request_4 = request with { nickName = "M" };
        var request_5 = request with { nickName = "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmM" };

        var request_6 = request with { email = "qweqwe123123123" };
        var request_7 = request with { email = "carre22222222222222222312333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333na@naver.com" };

        var request_8 = request with { password = "1233" };
        var request_9 = request with { password = "1233p2q2341231q23412311" };



        // Act
        var result_case_1 = await _createMemberValidator.ValidateAsync(request);
        var result_case_2 = await _createMemberValidator.ValidateAsync(request_2);
        var result_case_3 = await _createMemberValidator.ValidateAsync(request_3);
        var result_case_4 = await _createMemberValidator.ValidateAsync(request_4);
        var result_case_5 = await _createMemberValidator.ValidateAsync(request_5);
        var result_case_6 = await _createMemberValidator.ValidateAsync(request_6);
        var result_case_7 = await _createMemberValidator.ValidateAsync(request_7);
        var result_case_8 = await _createMemberValidator.ValidateAsync(request_8);
        var result_case_9 = await _createMemberValidator.ValidateAsync(request_9);



        // Assert
        Assert.True(result_case_1.IsValid);
        
        Assert.False(result_case_2.IsValid); //이름 20자 이상
        Assert.False(result_case_3.IsValid); //이름 2자 이하

        Assert.False(result_case_4.IsValid); //닉네임 2자 이하
        Assert.False(result_case_5.IsValid); //닉네임 20자 이상

        Assert.False(result_case_6.IsValid); //이메일 유효하지 않은 이메일 형식
        Assert.False(result_case_7.IsValid); //이메일 50자 이상

        Assert.False(result_case_8.IsValid); //암호 8자 이하
        Assert.False(result_case_9.IsValid); //암호 20자 이상

    }
}
