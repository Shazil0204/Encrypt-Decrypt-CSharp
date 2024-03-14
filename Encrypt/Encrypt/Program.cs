using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class EncryptionProgram
{
    // Encrypt data using AES algorithm
    public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
    {
        // Create AES encryption algorithm instance
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            // Create encryptor using AES algorithm
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create memory stream to store encrypted data
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                // Create crypto stream to perform encryption
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    // Write encrypted data to crypto stream
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }
                // Return encrypted data as byte array
                return msEncrypt.ToArray();
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the text you want to encrypt:");
        string originalText = Console.ReadLine();

        // Define encryption key and initialization vector (IV)
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF"); // 16-byte key for AES-128
        byte[] iv = Encoding.UTF8.GetBytes("1234567890ABCDEF"); // 16-byte IV (Initialization Vector)

        // Encrypt the input text
        byte[] encryptedBytes = Encrypt(originalText, key, iv);
        string encryptedText = Convert.ToBase64String(encryptedBytes);
        Console.WriteLine("Encrypted: " + encryptedText);

        // Pass encrypted text to decryption program
        Console.WriteLine("\nPass this encrypted text to the decryption program:");
        Console.WriteLine(encryptedText);
    }
}