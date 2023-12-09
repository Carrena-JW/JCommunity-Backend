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
    public string getMemberId() 
    {
        return this.Id.id.ToString();
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
            Password = password, 
            Email = email,
            CreatedMemberId = memberId.id.ToString(),
            LastUpdatedMemberId = memberId.id.ToString()

        };
        

        return member;
    }

}
