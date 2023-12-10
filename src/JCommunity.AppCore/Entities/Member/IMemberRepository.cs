using JCommunity.AppCore.Core.Abstractions;

namespace JCommunity.AppCore.Entities.Member;

public interface IMemberRepository : IRepository<Member>
{
    Member Add(Member member);
    void Update(Member member);
    void DeleteMember(Member member);
    Task<Member?> GetByIdAsync(MemberId memberId, CancellationToken token);

    Task<IEnumerable<Member>> GetAllMembersAsync(CancellationToken token);

    Task<bool> IsUniqueEmail(string email, CancellationToken token);

    Task<bool> IsUniqueNickName(string nickName, CancellationToken token);
}
