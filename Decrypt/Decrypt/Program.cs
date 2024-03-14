using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class DecryptionProgram
{
    // Decrypt data using AES algorithm
    public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
    {
        // Create AES decryption algorithm instance
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            // Create decryptor using AES algorithm
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create memory stream to store decrypted data
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                // Create crypto stream to perform decryption
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    // Read decrypted data from crypto stream
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("Enter the encrypted text you received:");
        string encryptedText = Console.ReadLine();

        // Define decryption key and initialization vector (IV)
        byte[] encryptedData = Convert.FromBase64String(encryptedText);
        byte[] key = Encoding.UTF8.GetBytes("0123456789ABCDEF"); // Same key used for encryption
        byte[] iv = Encoding.UTF8.GetBytes("1234567890ABCDEF"); // Same IV used for encryption

        // Decrypt the input text
        string decryptedText = Decrypt(encryptedData, key, iv);
        Console.WriteLine("Decrypted: " + decryptedText);
    }
}
