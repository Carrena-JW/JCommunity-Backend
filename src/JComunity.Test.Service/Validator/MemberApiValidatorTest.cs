namespace JComunity.Test.Service.EndpointTest;

public class MemberApiValidatorTest
{
    IValidator<SomeRequest> _validator;

    public MemberApiValidatorTest(IValidator<SomeRequest> validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    const string NAME_IS_UNIQUE_ERR_MSG = "name is must be unique";


    [Fact]
    async void SomePostActionTest()
    {
        // Arrange
        Console.WriteLine($"Max Length is {MemberRestriction.EMAIL_MAX_LENGTH}");
        var request = new SomeRequest(12, "이순신");

        // Act
        var result_case_1 = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result_case_1.IsValid);
        Assert.Contains(NAME_IS_UNIQUE_ERR_MSG, result_case_1.Errors.Select(e => e.ErrorMessage));

    }
}
