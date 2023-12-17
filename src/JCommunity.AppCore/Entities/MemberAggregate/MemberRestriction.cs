namespace JCommunity.AppCore.Entities.MemberAggregate;

public static class MemberRestriction
{
    public const int NAME_MIN_LENGTH = 2;
    public const int NAME_MAX_LENGTH = 20;

    public const int NICKNAME_MIN_LENGTH = 2;
    public const int NICKNAME_MAX_LENGTH = 20;

    public const int EMAIL_MAX_LENGTH = 50;

    public const int PASSWORD_MIN_LENGTH = 8;
    public const int PASSWORD_MAX_LENGTH = 20;
    public const int PASSWORD_HASHED_MAX_LENGTH = 600;
}
