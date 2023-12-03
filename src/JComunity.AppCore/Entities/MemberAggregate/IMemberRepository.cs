namespace JComunity.AppCore.Entities.MemberAggregate;

public interface IMemberRepository : IRepository<Member>
{
    Member Add(Member member);
    void Update(Member member);
    Task<Member?> GetByIdAsync(MemberId memberId);

    Task<bool> IsUniqueEmail(string email);

    Task<bool> IsUniqueNickName(string nickName);
}
