using EncryptionWithEntityFramework.Domain.Users;
using EncryptionWithEntityFramework.Infrastructure.Persistence.Configurations;
using EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace EncryptionWithEntityFramework.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly IAsymmetric _asymmetric;

    private readonly ISymmetric _symmetric;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAsymmetric asymmetric, ISymmetric symmetric) : base(options)
    {
        _asymmetric = asymmetric;
        _symmetric = symmetric;
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new UserConfiguration(_asymmetric, _symmetric).Configure(modelBuilder.Entity<User>());

        base.OnModelCreating(modelBuilder);
    }
}
