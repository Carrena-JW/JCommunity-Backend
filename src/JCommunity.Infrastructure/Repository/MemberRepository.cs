namespace JCommunity.Infrastructure.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _appDbContext;
    public IUnitOfWork UnitOfWork => _appDbContext;

    public MemberRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Member Add(Member member)
    {
        return _appDbContext.Members.Add(member).Entity;

    }

    public async Task<Member?> GetByIdAsync(Guid memberId, CancellationToken token)
    {
        return await _appDbContext.Members.FindAsync(memberId, token);

    }


    public async Task<bool> IsUniqueEmailAsync(string email, CancellationToken token)
    {
        return !await _appDbContext.Members.AnyAsync(c => c.Email == email, token);
    }

    public async Task<bool> IsUniqueNickNameAsync(string nickName, CancellationToken token)
    {
        return !await _appDbContext.Members.AnyAsync(c => c.NickName == nickName, token);
    }

    public async Task<IEnumerable<Member>> GetAllMembersAsync(CancellationToken token)
    {
        return await _appDbContext.Members.ToListAsync(token);
    }

    public void Remove(Member member) 
    {
        _appDbContext.Members.Remove(member);
    }
}
