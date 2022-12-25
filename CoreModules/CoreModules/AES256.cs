using System.Security.Cryptography;
using System.Text;

namespace CoreModules
{
    public static class AES256
    {
        /// <summary>
        /// 進行資料加、解密
        /// </summary>
        private static byte[] ReadStreamToCryptoByte(Stream stream, ICryptoTransform cryptoTransform)
        {
            using (var memoryStream = new MemoryStream())
            using (var cstream = new CryptoStream(stream, cryptoTransform, CryptoStreamMode.Read))
            {
                int length = 0;

                var buffer = new byte[1024 * 256];
                while ((length = cstream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, length);
                }

                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 取得 AES
        /// </summary>
        private static Aes GetAes(string cryptoKey)
        {
            var aes = Aes.Create();

            using(var sha256 = SHA256.Create())
            using (var sha384 = SHA384.Create())
            {
                byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                byte[] iv = sha384.ComputeHash(Encoding.UTF8.GetBytes(cryptoKey));
                aes.Key = key;
                aes.IV = new byte[iv.Length - 32];

                return aes;
            }
        }

        /// <summary>
        /// 字串加密
        /// </summary>
        /// <param name="source">欲加密字串</param>
        /// <param name="cryptoKey">加密金鑰</param>
        /// <returns>Base64String</returns>
        public static string EncryptString(this string source, string cryptoKey)
        {
            if (string.IsNullOrEmpty(source)) return source;

            using (var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes(source)))
            using (var aes = AES256.GetAes(cryptoKey))
            using (var cryptoTransform = aes.CreateEncryptor())
            {
                var encryptedBytes = 
                    AES256.ReadStreamToCryptoByte(sourceStream, cryptoTransform);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        /// <summary>
        /// 字串解密
        /// </summary>
        /// <param name="source">已加密的 Base64String</param>
        /// <param name="cryptoKey">加密金鑰</param>
        /// <returns></returns>
        public static string DecryptString(this string source, string cryptoKey)
        {
            try
            {
                using (var sourceStream = new MemoryStream(Convert.FromBase64String(source)))
                using (var aes = AES256.GetAes(cryptoKey))
                using (var cryptoTransform = aes.CreateDecryptor())
                {
                    var decryptedBytes = AES256.ReadStreamToCryptoByte(sourceStream, cryptoTransform);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
            catch (Exception)
            {
                return source;
            }
        }
    }
}