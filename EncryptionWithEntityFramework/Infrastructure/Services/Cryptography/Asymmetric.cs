using System.Text;

namespace EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;

public class Asymmetric : IAsymmetric
{
    public string Encrypt(string text)
    {
        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        byte[] hash;
        hash = System.Security.Cryptography.MD5.HashData(Encoding.UTF8.GetBytes(text));
        return Convert.ToBase64String(hash);
    }
}