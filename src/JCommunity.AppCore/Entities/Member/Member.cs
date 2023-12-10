namespace JCommunity.AppCore.Entities.Member;

public class Member : IAuditEntity, IAggregateRoot
{
    public MemberId Id { get; private set; } = new(Guid.NewGuid());
    public string Name { get; private set; } = string.Empty;
    public string NickName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public MemberStatus MemberStatus { get; private set; }
    public DateTime CreatedAt { get; private set; } = SystemTime.now();
    public string CreatedMemberId { get; private set; } = string.Empty;
    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();
    public string LastUpdatedMemberId { get; private set; } = string.Empty;
    public string GetMemberId() 
    {
        return this.Id.id.ToString();
    }

    public static MemberId CreateId (string id)
    {
        return new MemberId(Guid.Parse(id));
    }


    public static Member Create(
        string name,
        string nickName,
        string password,
        string email
        )
    {
        var memberId = new MemberId(Guid.NewGuid());
        Member member = new() 
        { 
            Id= memberId,
            Name = name, 
            NickName = nickName,
            Password = PasswordHasher.HashPassword(password), 
            Email = email,
            CreatedMemberId = memberId.id.ToString(),
            LastUpdatedMemberId = memberId.id.ToString()

        };
        

        return member;
    }

    public void SetNickName(string nickName)
    {
        if (this.NickName == nickName) return;
        
        this.NickName = nickName; 
    }

    public void SetPassword(string password)
    {
        var hashed = PasswordHasher.HashPassword(password);
        if (this.Password == hashed) return;
            
        this.Password = hashed;
    }

    public void SetEmail(string email)
    {
        if (this.Email == email) return;

        this.Email = email;
    }

    public void UpdateLastUpdateAt()
    {
        this.LastUpdatedAt = SystemTime.now();
    }
}
