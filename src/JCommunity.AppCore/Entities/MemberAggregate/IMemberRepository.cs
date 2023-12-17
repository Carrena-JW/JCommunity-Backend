namespace JCommunity.AppCore.Entities.MemberAggregate;

public interface IMemberRepository : IRepository<Member>
{
    Member Add(Member member);
    void Remove(Member member);
    Task<Member?> GetByIdAsync(Guid memberId, CancellationToken token);
    Task<IEnumerable<Member>> GetAllMembersAsync(CancellationToken token);
    Task<bool> IsUniqueEmailAsync(string email, CancellationToken token);
    Task<bool> IsUniqueNickNameAsync(string nickName, CancellationToken token);
}
