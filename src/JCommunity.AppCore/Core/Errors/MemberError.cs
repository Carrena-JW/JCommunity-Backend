namespace JCommunity.AppCore.Core.Errors;

public class MemberError
{
    public class NotFound : Error, IError
    {
        private const string MEMBER_NOT_FOUND_ERROR_CODE = "Member.NotFound";
        public NotFound(string id) : base()
        {
            this.Message = "Member not found.";
            
            WithMetadata("PropertyName", nameof(id));
            WithMetadata("AttemptedValue", id);
            WithMetadata("ErrorCode", MEMBER_NOT_FOUND_ERROR_CODE);
        }
    }

    public class InvalidError : Error, IError
    {
        public InvalidError()
        {
        }
    }
}
