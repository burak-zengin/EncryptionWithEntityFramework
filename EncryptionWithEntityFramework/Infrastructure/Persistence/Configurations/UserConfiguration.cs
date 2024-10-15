using EncryptionWithEntityFramework.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;

namespace EncryptionWithEntityFramework.Infrastructure.Persistence.Configurations;

public class UserConfiguration(IAsymmetric asymmetric, ISymmetric symmetric) : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property<string>(_ => _.Name).HasConversion(_ => symmetric.Encrypt(_), _ => symmetric.Decrypt(_));

        builder.Property<string>(_ => _.Surname).HasConversion(_ => symmetric.Encrypt(_), _ => symmetric.Decrypt(_));

        builder.Property<string>(_ => _.Password).HasConversion(_ => asymmetric.Encrypt(_), _ => _);
    }
}
