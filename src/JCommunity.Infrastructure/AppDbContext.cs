namespace JCommunity.Infrastructure;

public class AppDbContext : DbContext, IUnitOfWork
{

    public DbSet<Member> Members { get; set; }
    public DbSet<TopicCategory> TopicCategories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken ct = default)
    {
        _ = await base.SaveChangesAsync(ct);

        return true;
    }
}
