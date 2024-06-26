using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// 문자열 AES 암호화 클래스
/// </summary>
public static class AesEncryption {

    /// <summary>
    /// 키를 바탕으로 복호화
    /// </summary>
    /// <param name="plainText">복호화 대상 문자열</param>
    /// <param name="key">복호화 키</param>
    /// <returns></returns>
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

    /// <summary>
    /// 암호 해독
    /// </summary>
    /// <param name="cipherText">해독할 문자열</param>
    /// <param name="key">복호화 키</param>
    /// <returns></returns>
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

    /// <summary>
    /// 키를 바탕으로 암호 생성
    /// </summary>
    /// <param name="key">키</param>
    /// <returns></returns>
    private static byte[] GetAesKey(string key) {
        using (SHA256 sha256 = SHA256.Create()) {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}
