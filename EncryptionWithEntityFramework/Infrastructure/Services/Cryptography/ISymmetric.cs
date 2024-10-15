namespace EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;

public interface ISymmetric
{
    string Encrypt(string text);

    string Decrypt(string text);
}
