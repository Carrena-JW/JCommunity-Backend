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

    public class EmailNotUnique : Error, IError
    {
        private const string MEMBER_EMAIL_NOT_UNIQUE_ERROR_CODE = "Member.Email.NotUnique";
        public EmailNotUnique(string email) : base()
        {
            this.Message = "Email address is not unique.";

            WithMetadata("PropertyName", nameof(email));
            WithMetadata("AttemptedValue", email);
            WithMetadata("ErrorCode", MEMBER_EMAIL_NOT_UNIQUE_ERROR_CODE);
        }
    }

    public class NicknameNotUnique : Error, IError
    {
        private const string MEMBER_NICKNAME_NOT_UNIQUE_ERROR_CODE = "Member.Nickname.NotUnique";
        public NicknameNotUnique(string nickname) : base()
        {
            this.Message = "Email address is not unique.";

            WithMetadata("PropertyName", nameof(nickname));
            WithMetadata("AttemptedValue", nickname);
            WithMetadata("ErrorCode", MEMBER_NICKNAME_NOT_UNIQUE_ERROR_CODE);
        }
    }

}
