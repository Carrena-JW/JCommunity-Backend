namespace JCommunity.AppCore.Core.Errors;

public class ValidationError : Error, IError
{
    public ValidationError(List<ValidationFailure> errors) : base()
    {
        this.Message = "Invalid parameter error";

        this.Reasons.AddRange(errors.Select(error =>
            new Error(error.ErrorMessage)
                .WithMetadata("AttemptedValue", error.AttemptedValue)
                .WithMetadata("PropertyName", error.PropertyName)
                .WithMetadata("ErrorCode", error.ErrorCode)
        ));
    }
}
