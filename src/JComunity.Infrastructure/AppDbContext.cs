namespace JComunity.Infrastructure;

public class AppDbContext : DbContext
{

    public DbSet<Member> Member { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=password1;Server=localhost;Port=5432;database=postgres; Integrated Security=true;Pooling=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
