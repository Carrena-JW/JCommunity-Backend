namespace JCommunity.AppCore.Entities.Member;

public class Member : IAuditEntity, IAggregateRoot
{
    public Guid Id { get; private set; } 
    public string Name { get; private set; } = string.Empty;
    public string NickName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public MemberStatus MemberStatus { get; private set; } = MemberStatus.Active;
    public DateTime CreatedAt { get; private set; } = SystemTime.now();
    public string CreatedMemberId { get; private set; } = string.Empty;
    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();
    public string LastUpdatedMemberId { get; private set; } = string.Empty;

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
        if (this.NickName == nickName) return;
        
        this.NickName = nickName; 
    }

    public void UpdatePassword(string password)
    {
        var hashed = PasswordHasher.HashPassword(password);
        if (this.Password == hashed) return;
            
        this.Password = hashed;
    }

    public void UpdateEmail(string email)
    {
        if (this.Email == email) return;

        this.Email = email;
    }

    public void UpdateLastUpdateAt()
    {
        this.LastUpdatedAt = SystemTime.now();
    }
}
