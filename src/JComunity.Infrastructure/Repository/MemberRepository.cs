namespace JComunity.Infrastructure.Repository;

public class MemberRepository : IMemberRepository
{
    public IUnitOfWork UnitOfWork => (IUnitOfWork)_appDbContext;
    private readonly AppDbContext _appDbContext;

    public MemberRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Member Add(Member member)
    {
        return _appDbContext.Members.Add(member).Entity;
    }

    public async Task<Member?> GetByIdAsync(MemberId memberId)
    {
        return await _appDbContext.Members.FindAsync(memberId);
    }

    public void Update(Member member)
    {
        _appDbContext.Entry(member).State = EntityState.Modified;
    }
}
