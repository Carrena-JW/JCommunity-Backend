namespace JCommunity.Infrastructure.Entitybuilderurations;

internal class MemberTypebuilderuration : IEntityTypeConfiguration<Member>

{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("members");

        // Id
        builder.HasKey(builder => builder.Id);
        builder.Property(builder => builder.Id)
            .HasConversion(
                Id => Id.ToString(),
                value => Guid.Parse(value)
            );

        // Name
        builder.Property(builder => builder.Name)
            .HasMaxLength(MemberRestriction.NAME_MAX_LENGTH);

        // NickName
        builder.HasIndex(builder => builder.NickName).IsUnique();
        builder.Property(builder => builder.NickName)
            .HasMaxLength(MemberRestriction.NICKNAME_MAX_LENGTH);


        // Email
        builder.HasIndex(builder => builder.Email).IsUnique();
        builder.Property(builder => builder.Email)
            .HasMaxLength(MemberRestriction.EMAIL_MAX_LENGTH);

        // Password
        builder.Property(builder => builder.Password)
            .HasMaxLength(MemberRestriction.PASSWORD_HASHED_MAX_LENGTH);
    }
}
