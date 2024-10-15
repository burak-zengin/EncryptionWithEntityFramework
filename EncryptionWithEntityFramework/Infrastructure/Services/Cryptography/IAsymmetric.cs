namespace EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;

public interface IAsymmetric
{
    string Encrypt(string text);
}
