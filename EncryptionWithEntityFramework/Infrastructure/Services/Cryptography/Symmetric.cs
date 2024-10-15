using System.Security.Cryptography;
using System.Text;

namespace EncryptionWithEntityFramework.Infrastructure.Services.Cryptography;

public class Symmetric : ISymmetric
{
    public string Key => "1234567890AB1234567890AB";

    public string Encrypt(string text)
    {
        ArgumentNullException.ThrowIfNull(Key, "Encryption key");

        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        byte[] encrypted;

        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(Key);
            aesAlg.Mode = CipherMode.ECB;
            aesAlg.Padding = PaddingMode.PKCS7;

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(text);
            }
            encrypted = msEncrypt.ToArray();
        }

        return Convert.ToBase64String(encrypted);
    }

    public string Decrypt(string text)
    {
        ArgumentNullException.ThrowIfNull(Key, "Encryption key");

        if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        var descriptor = aes.CreateDecryptor(aes.Key, aes.IV);

        var buffer = Convert.FromBase64String(text);
        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, descriptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}
