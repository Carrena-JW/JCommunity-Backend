namespace JCommunity.AppCore.Entities.MemberAggregate;

public interface IMemberRepository : IRepository<Member>
{
    Member Add(Member member);
    void Update(Member member);
    Task<Member?> GetByIdAsync(MemberId memberId, CancellationToken token);

    Task<bool> IsUniqueEmail(string email, CancellationToken token);

    Task<bool> IsUniqueNickName(string nickName, CancellationToken token);
}
