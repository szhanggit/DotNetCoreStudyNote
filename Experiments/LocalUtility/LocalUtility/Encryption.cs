using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tx2.Adora.Utils.Encryption
{


    /// Used for Example purpose on how to decrypt a received WorkKey
    /// SecurityKey = 4087CBA9DB259CC1F9AF21A671384B12
    /// WorkKey = 2BF1760435D38D9647481ED01589CFC0
    /// Encrypted WorkKey = U4jsLbkET+TvaaJzScUgBbcqZw9ykK7w7FptejcRCXh0mQL8zhM1Jw==

    /// <summary>
    /// Class for MD5 signature
    /// </summary>
    public static class MD5Helper
    {
        /// <summary>
        /// get MD5 hash. Used for Checksum calculation.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHash(string value)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// For Triple DES encryption. Do not use TripleDESCryptoServiceProvider, use this class instead.
    /// </summary>
    public static class TDesHelper
    {
        /// <summary>
        /// Encrypt data with triple DES
        /// When I use my Example and call: TDesHelper.Encrypt("2BF1760435D38D9647481ED01589CFC0", "4087CBA9DB259CC1F9AF21A671384B12")
        /// Then I get: U4jsLbkET+TvaaJzScUgBbcqZw9ykK7w7FptejcRCXh0mQL8zhM1Jw==
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt, string key)
        {
            //Verify that the key is not null
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Security Key is not correct");
            }

            /// Get the Bytes of the content to Encrypt. Format used is UTF8
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncrypt);

            /// Create keys parameters
            byte[] k1 = null, k2 = null, k3 = null, iv = null;
            /// Get keys values by splitting 4087CBA9DB259CC1F9AF21A671384B12 into sub keys. Here two keys with k1 = k3
            /// k1 = k3 = HexaToBytes(4087CBA9DB259CC1) and k2 = iv = HexaToBytes(F9AF21A671384B12)
            /// iv is just a vector used by the cipher
            /// Important, for a 32char key, k1 = k3
            GetKeyBytes(key, ref k1, ref k2, ref k3, ref iv);

            /// Do the TDES Encryption, then convert the result to base64 format.
            /// (Encryption, k1, iv, Padding PKCS7) -> (Decryption, k2, iv, Padding NO) -> (Encryption, k3, iv, Padding NO) -> (To string base64)
            return Convert.ToBase64String(Encrypt(Decrypt(Encrypt(inputByteArray, k1, iv, PaddingMode.PKCS7), k2, iv, PaddingMode.None), k3, iv, PaddingMode.None));
        }

        /// <summary>
        /// Decrypt data with Triple DES
        /// When I use my Example and call: TDesHelper.Decrypt("U4jsLbkET+TvaaJzScUgBbcqZw9ykK7w7FptejcRCXh0mQL8zhM1Jw==", "4087CBA9DB259CC1F9AF21A671384B12")
        /// Then I get: 2BF1760435D38D9647481ED01589CFC0
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt, string key)
        {
            //Verify that the key is not null
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Security Key is not correct");
            }

            /// get bytes values from the base64 encoded string
            byte[] inputByteArray = Convert.FromBase64String(toDecrypt);

            byte[] k1 = null, k2 = null, k3 = null, iv = null;
            /// Get keys values by splitting 4087CBA9DB259CC1F9AF21A671384B12 into sub keys. Here two keys with k1 = k3
            /// k1 = k3 = HexaToBytes(4087CBA9DB259CC1) and k2 = iv = HexaToBytes(F9AF21A671384B12)
            /// iv is just a vector used by the cipher
            /// Important, for a 32char key, k1 = k3
            GetKeyBytes(key, ref k1, ref k2, ref k3, ref iv);

            /// Do the TDES Encryption, this process is the opposite to the encryption process.
            /// (Decryption, k3, iv, Padding NO) -> (Encryption, k2, iv, Padding NO) -> (Decryption, k1, iv, Padding PKCS7) -> (To string UTF8)
            return Encoding.UTF8.GetString(Decrypt(Encrypt(Decrypt(inputByteArray, k3, iv, PaddingMode.None), k2, iv, PaddingMode.None), k1, iv, PaddingMode.PKCS7));
        }

        /// <summary>
        /// Get bytes from an Hexa string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string key)
        {
            byte[] results = new byte[8];
            var array = key.ToCharArray();

            for (int i = 1; i < array.Length; i++)
            {
                if (i % 2 != 0)
                {
                    /// take 4bits from the left char and 4bits from the right char to do a byte (8bits) that will be part of the key
                    results[i / 2] = (byte)((byte)(byte.Parse(array[i - 1].ToString(), NumberStyles.HexNumber) << 4) | (byte.Parse(array[i].ToString(), NumberStyles.HexNumber)));
                }
            }

            return results;
        }

        /// <summary>
        /// Decrypt with DES
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <param name="keyBytes"></param>
        /// <param name="ivBytes"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        private static byte[] Decrypt(byte[] inputByteArray, byte[] keyBytes, byte[] ivBytes, PaddingMode paddingMode)
        {
            /// Use a DES provider
            using (var des = new DESCryptoServiceProvider())
            {
                /// Configure the provider
                des.Padding = paddingMode;
                des.Key = keyBytes;
                des.IV = ivBytes;

                /// Do the encryption using a MemoryStream
                using (var ms = new MemoryStream())
                {
                    /// Do the Encryption using a CryptoStream with our DESCryptoServiceProvider
                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        /// Write in Crypto stream then flush in memorystream
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }

                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Encrypt with DES
        /// </summary>
        /// <param name="inputByteArray"></param>
        /// <param name="keyBytes"></param>
        /// <param name="ivBytes"></param>
        /// <param name="paddingMode"></param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] inputByteArray, byte[] keyBytes, byte[] ivBytes, PaddingMode paddingMode)
        {
            /// Use a DES provider
            using (var des = new DESCryptoServiceProvider())
            {
                /// Configure the provider
                des.Padding = paddingMode;
                des.Key = keyBytes;
                des.IV = ivBytes;

                /// Do the Decryption using a MemoryStream
                using (var ms = new MemoryStream())
                {
                    /// Do the Decryption using a CryptoStream with our DESCryptoServiceProvider
                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        /// Write in Crypto stream then flush in memorystream
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                    }

                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Get keys and iv for TDES Encryption.
        /// 16 char -> k1 = k2 = k3 = iv
        /// 32 char -> k1 = k3, k2 = iv
        /// 48 char -> k1, k2, k3 = iv
        /// </summary>
        /// <param name="key"></param>
        /// <param name="k1"></param>
        /// <param name="k2"></param>
        /// <param name="k3"></param>
        /// <param name="iv"></param>
        private static void GetKeyBytes(string key, ref byte[] k1, ref byte[] k2, ref byte[] k3, ref byte[] iv)
        {
            switch (key.Length)
            {
                case 16:
                    k1 = GetBytes(key);
                    k2 = k1;
                    k3 = k2;
                    iv = k1;
                    break;
                case 32:
                    k1 = GetBytes(key.Substring(0, 16));
                    k2 = GetBytes(key.Substring(16, 16));
                    k3 = k1;
                    iv = k2;
                    break;
                case 48:
                    k1 = GetBytes(key.Substring(0, 16));
                    k2 = GetBytes(key.Substring(16, 16));
                    k3 = GetBytes(key.Substring(32, 16));
                    iv = k3;
                    break;
            }
        }
    }
}