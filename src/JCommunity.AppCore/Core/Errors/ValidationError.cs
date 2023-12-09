namespace JCommunity.AppCore.Core.Errors;

public class ValidationError : Error, IError
{
    private const string INVALID_MESSAGE = "Invalid parameter error";
    public ValidationError(List<ValidationFailure> errors) : base()
    {
        this.Message = INVALID_MESSAGE;

        this.Reasons.AddRange(errors.Select(error =>
            new Error(error.ErrorMessage)
                .WithMetadata("AttemptedValue", error.AttemptedValue)
                .WithMetadata("PropertyName", error.PropertyName)
                .WithMetadata("ErrorCode", error.ErrorCode)
        ));
    }
}
