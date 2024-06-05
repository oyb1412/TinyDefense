using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// ���ڿ� AES ��ȣȭ Ŭ����
/// </summary>
public static class AesEncryption {

    /// <summary>
    /// Ű�� �������� ��ȣȭ
    /// </summary>
    /// <param name="plainText">��ȣȭ ��� ���ڿ�</param>
    /// <param name="key">��ȣȭ Ű</param>
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
    /// ��ȣ �ص�
    /// </summary>
    /// <param name="cipherText">�ص��� ���ڿ�</param>
    /// <param name="key">��ȣȭ Ű</param>
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
    /// Ű�� �������� ��ȣ ����
    /// </summary>
    /// <param name="key">Ű</param>
    /// <returns></returns>
    private static byte[] GetAesKey(string key) {
        using (SHA256 sha256 = SHA256.Create()) {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
        }
    }
}
