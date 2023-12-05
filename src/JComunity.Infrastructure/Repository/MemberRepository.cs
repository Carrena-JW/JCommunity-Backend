namespace JComunity.Infrastructure.Repository;

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

    public async Task<Member?> GetByIdAsync(MemberId memberId, CancellationToken token)
    {
        return await _appDbContext.Members.FindAsync(memberId);
    }

    public void Update(Member member)
    {
        _appDbContext.Entry(member).State = EntityState.Modified;
    }

    public async Task<bool> IsUniqueEmail(string email, CancellationToken token)
    {
        return !await _appDbContext.Members.AnyAsync(c => c.Email == email);
    }

    public async Task<bool> IsUniqueNickName(string nickName, CancellationToken token)
    {
        return !await _appDbContext.Members.AnyAsync(c => c.NickName == nickName);
    }
}
