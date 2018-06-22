using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SimpleCM.Tools
{
    class EncryptUtil
    {
        private static string key = "abcdef1234567890";
        //默认密钥向量
        private static byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public static string Encrypt(string path)
        {
            try
            {
                //分组加密算法
                SymmetricAlgorithm des = Rijndael.Create();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(path);//得到需要加密的字节数组
                //设置密钥及密钥向量
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = iv;
                byte[] cipherBytes = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        cipherBytes = ms.ToArray();//得到加密后的字节数组
                        cs.Close();
                        ms.Close();
                    }
                }
                return Convert.ToBase64String(cipherBytes);
            }
            catch (Exception ex)
            {
                Log.D("", "Encrypt error:" + ex.Message);
                return path;
            }
        }

        public static string Decrypt(string path)
        {
            try
            {
                byte[] cipherText = Convert.FromBase64String(path);

                SymmetricAlgorithm des = Rijndael.Create();
                des.Key = Encoding.UTF8.GetBytes(key);
                des.IV = iv;
                byte[] decryptBytes = new byte[cipherText.Length];
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        cs.Read(decryptBytes, 0, decryptBytes.Length);
                        cs.Close();
                        ms.Close();
                    }
                }
                return Encoding.UTF8.GetString(decryptBytes).Replace("\0", "");   ///将字符串后尾的'\0'去掉
            }
            catch (Exception ex)
            {
                Log.D("", "Decrypt error:" + ex.Message);
                return path;
            }
        }
    }
}
