using JCommunity.AppCore.Entities.Member;
using JCommunity.AppCore.Entities.Topics;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace JCommunity.Test.Core.Utils;

public class MemoryDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Topic> Topics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // 인메모리 데이터베이스 사용
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JCommunity.Infrastructure.AssemblyReference).Assembly);
    }
}
 