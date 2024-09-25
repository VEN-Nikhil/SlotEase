using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SlotEase.Domain.Helpers;

public static class CryptographyHelper
{
    private static readonly string aesKeySecret = "ZENPBzl5ASkiRahKi=JHK@!$RTYEWQRGH";
    private static readonly string aesIVSecret = "auf6549pum5";

    private static readonly byte[] _aesKey = GenerateKey();
    private static readonly byte[] _aesIV = GenerateIV();

    #region With AES 
    public static string Encrypt(string plainText)
    {
        byte[] encryptedBytes;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter writer = new(cryptoStream))
                    {
                        writer.Write(plainText);
                    }

                    encryptedBytes = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(encryptedBytes);
    }
    public static string Decrypt(string cipherText)
    {
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        string decryptedText;

        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = _aesKey;
            aesAlg.IV = _aesIV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream memoryStream = new(cipherBytes))
            {
                using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader reader = new(cryptoStream))
                    {
                        decryptedText = reader.ReadToEnd();
                    }
                }
            }
        }

        return decryptedText;
    }
    private static byte[] GenerateKey()
    {
        const int KeySizeInBytes = 32; // AES-256 requires a 256-bit (32 bytes) key

        using (SHA256 sha256 = SHA256.Create())
        {
            string secretKey = aesKeySecret;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] hashedBytes = sha256.ComputeHash(keyBytes);

            byte[] key = new byte[KeySizeInBytes];
            Array.Copy(hashedBytes, key, KeySizeInBytes);

            return key;
        }
    }
    private static byte[] GenerateIV()
    {
        const int IVSizeInBytes = 16; // AES block size is 128 bits (16 bytes)

        using (SHA256 sha256 = SHA256.Create())
        {
            string secretKey = aesIVSecret;

            byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
            byte[] hashedBytes = sha256.ComputeHash(keyBytes);

            byte[] key = new byte[IVSizeInBytes];
            Array.Copy(hashedBytes, key, IVSizeInBytes);

            return key;
        }
    }

    #endregion With AES
}
