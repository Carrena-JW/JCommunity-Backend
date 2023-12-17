namespace JCommunity.AppCore.Entities.Member;

public class Member : AggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public string NickName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public MemberStatus MemberStatus { get; private set; } = MemberStatus.Active;
   
    public string GetMemberId() 
    {
        return this.Id.ToString();
    }

    public static Guid ConvertMemberIdFromString (string id)
    {
        return Guid.Parse(id);
    }


    public static Member Create(
        string name,
        string nickName,
        string password,
        string email
        )
    {
        var memberId = Guid.NewGuid();
        Member member = new()
        {
            Id = Guid.NewGuid(),
            Name = name,
            NickName = nickName,
            Password = PasswordHasher.HashPassword(password),
            Email = email,
            MemberStatus = MemberStatus.Active,
            CreatedMemberId = memberId.ToString(),
            LastUpdatedMemberId = memberId.ToString()

        };
        

        return member;
    }

    public void UpdateNickname(string nickName)
    {
        if (this.NickName != nickName) this.NickName = nickName; 
    }

    public void UpdatePassword(string password)
    {
        var hashed = PasswordHasher.HashPassword(password);
        if (this.Password != hashed) this.Password = hashed;
    }

    public void UpdateEmail(string email)
    {
        if (this.Email != email) this.Email = email;
    }

    public void UpdateLastUpdateAt()
    {
        this.LastUpdatedAt = SystemTime.now();
    }
}
