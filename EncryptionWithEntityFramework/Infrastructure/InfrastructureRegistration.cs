using EncryptionWithEntityFramework.Infrastructure.Persistence;
using EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace EncryptionWithEntityFramework.Infrastructure;

public static class InfrastructureRegistration
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ISymmetric, Symmetric>();
        services.AddSingleton<IAsymmetric, Asymmetric>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSql"));
        });
    }
}
