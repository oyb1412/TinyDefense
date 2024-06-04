using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public static class AesEncryption {

    public static string Encrypt(string plainText, string key) {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create()) {
            aes.Key = GetAesKey(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream()) {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream)) {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }

        return Convert.ToBase64String(array);
    }

    public static string Decrypt(string cipherText, string key) {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create()) {
            aes.Key = GetAesKey(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer)) {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream)) {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
    }

    private static byte[] GetAesKey(string key) {
        using (SHA256 sha256 = SHA256.Create()) {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }

}
