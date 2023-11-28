

using JComunity.AppCore.Abstractions;
using JComunity.AppCore.Restrictions;

namespace JComunity.Domain.Entities.Users;

public class Member : IAuditEntity
{

    public MemberId Id { get; private set; }
    public Name Name { get; private set; }
    public NickName NickName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public MemberStatus MemberStatus { get; private set; }

    public DateTime CreatedAt { get; private set; } = SystemTime.now();

    public MemberId CreatedMemberId { get; private set; } 

    public DateTime LastUpdatedAt { get; private set; } = SystemTime.now();

    public MemberId LastUpdatedMemberId { get; private set; }

    internal Member(
        string name,
        string nickName,
        string password,
        string email)
    {
        //Required values
        Id = new MemberId(Guid.NewGuid()); 
        Name = new Name(name);
        NickName = new NickName(nickName);
        Email = new Email(email);
        Password = new Password(PasswordHasher.HashPassword(password));
        MemberStatus = MemberStatus.Active;

        //Audit
        CreatedMemberId = this.Id; 
        LastUpdatedMemberId = this.Id;


        
    }

    internal static Member Create(
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
