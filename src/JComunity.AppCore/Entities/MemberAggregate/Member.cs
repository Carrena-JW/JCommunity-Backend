

using JComunity.AppCore.Abstractions;
using JComunity.AppCore.Restrictions;

namespace JComunity.Domain.Entities.Users;

public class Member : IAuditEntity, IAggregateRoot
{

    public MemberId Id { get; private set; }
    public string Name { get; private set; }
    public string NickName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public MemberStatus MemberStatus { get; private set; }

    public DateTime CreatedAt { get; private set; } = SystemTime.now();

    public string CreatedMemberId { get; private set; } 

    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();

    public string LastUpdatedMemberId { get; private set; }

    internal Member(
        string name,
        string nickName,
        string password,
        string email)
    {
        //Required values
        Id = new MemberId(Guid.NewGuid()); 
        Name = name;
        NickName = nickName;
        Email =email;
        Password = PasswordHasher.HashPassword(password);
        MemberStatus = MemberStatus.Active;

        //Audit
        CreatedMemberId = this.Id.id.ToString(); 
        LastUpdatedMemberId = this.Id.id.ToString();
        
    }

    public static Member Create(
        string name,
        string nickName,
        string password,
        string email
        )
    {
        var member = new Member(name, nickName, password, email);
        return member;
    }
}
