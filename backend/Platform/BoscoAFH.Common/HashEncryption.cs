using System.Security.Cryptography;

namespace BoscoAFH.Common
{
    public class EncryptionHelper
    {
        // Example AES Key and IV (You should generate and store them securely)
        private static readonly string EncryptionKey = "1a2b3c4d5e6f7a8B9c0d1E2f3a4b5c6D"; // 32 bytes for AES-256

        private static readonly string IV = "5F4D3c2b1a0X9d8c"; // 16 bytes for AES-128

        public static string GetDefaultEncryptPassword()
        {
            return "s6HOsnGLauCoXohGLW/9egwuv/RWuDUnetpntZPWLBuvwYkGgEgD7w=="; //"bbh@2024"
                                                                               // return "bmtXFHGrzYQhkp8FZZBuD8ZGZvShwzBrqJA6bX9ud22LZRfR39uGXw=="; //"1"
        }

        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;  // Return empty string instead of null
            }

            byte[] salt = new byte[20];
            byte[] key;
            byte[] ret = new byte[40];

            try
            {
                RandomNumberGenerator.Fill(salt);

                using (var hashBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256))
                {
                    key = hashBytes.GetBytes(20);
                }

                Buffer.BlockCopy(salt, 0, ret, 0, 20);
                Buffer.BlockCopy(key, 0, ret, 20, 20);

                return Convert.ToBase64String(ret);
            }
            finally
            {
                Array.Clear(salt, 0, salt.Length);
                Array.Clear(ret, 0, ret.Length);
            }
        }

        public static string EncryptValue(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
            aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            // Convert to Base64 string, then apply Base64Url encoding
            string base64 = Convert.ToBase64String(msEncrypt.ToArray());
            return ToBase64Url(base64); // Apply Base64Url encoding
        }

        /// <summary>
        /// accepts an encrypted string and returns the string as plain text
        /// </summary>
        /// <param name="Msg"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string DecryptValue(string base64UrlCipherText)
        {
            if (string.IsNullOrEmpty(base64UrlCipherText))
            {
                return "0";
            }
            try
            {
                // Convert from Base64Url to Base64
                string base64CipherText = FromBase64Url(base64UrlCipherText);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
                aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64CipherText)); // Convert Base64 back to byte array
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                return srDecrypt.ReadToEnd(); // Return decrypted plaintext
            }
            catch (Exception)
            {
                return "0";
            }
        }

        // Convert back from Base64Url to Base64
        public static string FromBase64Url(string base64Url)
        {
            base64Url = base64Url.Replace('-', '+').Replace('_', '/');
            return string.Concat(base64Url, "==".AsSpan(0, (4 - base64Url.Length % 4) % 4)); // Padding to correct length
        }

        // Convert to Base64Url (for safe URL transmission)
        public static string ToBase64Url(string base64)
        {
            //The characters ==> { ?, &, #, and % } are not part of the Base64 character set by design.
            return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        public static bool VerifyPassword(string passwordHash, string password)
        {
            if (string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            byte[] salt = new byte[20];
            byte[] key = new byte[20];
            byte[] hash = Convert.FromBase64String(passwordHash);

            try
            {
                Buffer.BlockCopy(hash, 0, salt, 0, 20);
                Buffer.BlockCopy(hash, 20, key, 0, 20);

                byte[] newKey;
                using (var hashBytes = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256)) // ✅ Secure iterations & SHA256
                {
                    newKey = hashBytes.GetBytes(20);
                }

                return newKey.SequenceEqual(key);
            }
            finally
            {
                Array.Clear(salt, 0, salt.Length);
                Array.Clear(key, 0, key.Length);
                Array.Clear(hash, 0, hash.Length);
            }
        }
    }

    //Advanced Encryption Standard Algorithm for High Security -> Created By [Albin_Anthony]
    public class AESHelper
    {
        // Example AES Key and IV (You should generate and store them securely)
        private static readonly string EncryptionKey = "1a2b3c4d5e6f7a8B9c0d1E2f3a4b5c6D"; // 32 bytes for AES-256

        private static readonly string IV = "5F4D3c2b1a0X9d8c"; // 16 bytes for AES-128

        private static string GenerateNewString(string chars)
        {
            Random rand = new();

            // Create a character array to hold the resulting string
            char[] resultArray = new char[chars.Length];

            // Dynamically populate the resultArray with random characters from 'chars'
            for (int i = 0; i < resultArray.Length; i++)
            {
                resultArray[i] = chars[rand.Next(chars.Length)];
            }

            // Convert the char array into a string
            return new string(resultArray);
        }

        static AESHelper()
        {
            IV = GenerateNewString(IV);
            EncryptionKey = GenerateNewString(EncryptionKey);
        }

        // Convert to Base64Url (for safe URL transmission)
        public static string ToBase64Url(string base64)
        {
            //The characters ==> { ?, &, #, and % } are not part of the Base64 character set by design.
            return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        // Convert back from Base64Url to Base64
        public static string FromBase64Url(string base64Url)
        {
            base64Url = base64Url.Replace('-', '+').Replace('_', '/');
            return string.Concat(base64Url, "==".AsSpan(0, (4 - base64Url.Length % 4) % 4)); // Padding to correct length
        }

        // Encrypt the plaintext using AES algorithm
        public static string EncryptData(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
            aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using MemoryStream msEncrypt = new MemoryStream();
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            // Convert to Base64 string, then apply Base64Url encoding
            string base64 = Convert.ToBase64String(msEncrypt.ToArray());
            return ToBase64Url(base64); // Apply Base64Url encoding
        }

        //aysnc method
        public static async Task<string> EncryptDataAsync(string plainText)
        {
            return await Task.Run(() =>
            {
                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                aesAlg.IV = Encoding.UTF8.GetBytes(IV);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using MemoryStream msEncrypt = new MemoryStream();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                string base64 = Convert.ToBase64String(msEncrypt.ToArray());
                return ToBase64Url(base64);
            });
        }

        // Decrypt the ciphertext back to plaintext
        public static string DecryptData(string base64UrlCipherText)
        {
            if (string.IsNullOrEmpty(base64UrlCipherText))
            {
                return "0";
            }
            try
            {
                // Convert from Base64Url to Base64
                string base64CipherText = FromBase64Url(base64UrlCipherText);

                using Aes aesAlg = Aes.Create();
                aesAlg.Key = Encoding.UTF8.GetBytes(EncryptionKey); // Ensure 32 bytes (256-bit key)
                aesAlg.IV = Encoding.UTF8.GetBytes(IV); // Ensure 16 bytes IV

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(base64CipherText)); // Convert Base64 back to byte array
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                return srDecrypt.ReadToEnd(); // Return decrypted plaintext
            }
            catch (Exception)
            {
                return "0";
            }
        }
    }

    public static class AESHelperUtility
    {
        // Tell the helper how your key/IV are stored:
        // "raw"  => plain ASCII/UTF8 chars (length must be exact: 32 for key, 16 for IV)
        // "base64" => convert from Base64 to bytes
        // "hex"  => convert from hex to bytes
        private const string KeyFormat = "raw";   // change to "base64" or "hex" if that’s what you store

        private const string IvFormat = "raw";   // change to "base64" or "hex" if you store it that way

        //public static string EncryptionKey = "<YOUR_KEY>"; // 32 chars if KeyFormat="raw"
        //public static string IV = "<YOUR_IV>";  // 16 chars if IvFormat="raw"

        // Example AES Key and IV (You should generate and store them securely)
        private static readonly string EncryptionKey = "1a2b3c4d5e6f7a8B9c0d1E2f3a4b5c6D"; // 32 bytes for AES-256

        private static readonly string IV = "5F4D3c2b1a0X9d8c"; // 16 bytes for AES-128

        public static string DecryptData(string base64UrlCipherText)
        {
            // 1) Base64URL -> bytes (safe even if '=' padding is missing)
            byte[] cipher = Base64UrlDecodeToBytes(base64UrlCipherText);

            // 2) Key/IV bytes
            byte[] key = Parse(EncryptionKey, KeyFormat);
            byte[] iv = Parse(IV, IvFormat);

            // 3) Sanity
            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new CryptographicException($"Invalid AES key length {key.Length}.");
            }
            if (iv.Length != 16)
            {
                throw new CryptographicException($"Invalid AES IV length {iv.Length}.");
            }
            // 4) Decrypt
            using Aes aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.BlockSize = 128;
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cipher, 0, cipher.Length);
                cs.FlushFinalBlock(); // throws if padding is bad -> means input/key/iv mismatch
            }
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static string EncryptData(string plainText)
        {
            byte[] key = Parse(EncryptionKey, KeyFormat);
            byte[] iv = Parse(IV, IvFormat);

            if (key.Length != 16 && key.Length != 24 && key.Length != 32)
            {
                throw new CryptographicException($"Invalid AES key length {key.Length}.");
            }
            if (iv.Length != 16)
            {
                throw new CryptographicException($"Invalid AES IV length {iv.Length}.");
            }
            using Aes aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.BlockSize = 128;
            aesAlg.Key = key;
            aesAlg.IV = iv;

            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }

            // Base64 -> Base64URL for links
            return ToBase64Url(Convert.ToBase64String(ms.ToArray()));
        }

        public static async Task<string> EncryptDataAsync(string plainText, CancellationToken cancellationToken = default)
        {
            // Use the same key/IV logic as your DecryptData
            byte[] key = Encoding.UTF8.GetBytes(EncryptionKey);
            byte[] iv = Encoding.UTF8.GetBytes(IV);

            if (!(key.Length == 16 || key.Length == 24 || key.Length == 32))
            {
                throw new CryptographicException($"Invalid AES key length {key.Length}.");
            }
            if (iv.Length != 16)
            {
                throw new CryptographicException($"Invalid AES IV length {iv.Length}.");
            }

            using var aesAlg = Aes.Create();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;
            aesAlg.BlockSize = 128;
            aesAlg.Key = key;
            aesAlg.IV = iv;

            byte[] input = Encoding.UTF8.GetBytes(plainText);

            using var msEncrypt = new MemoryStream();
            using (var csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write, leaveOpen: true))
            {
                // ✅ Updated to CA1835-compliant overload
                await csEncrypt.WriteAsync(input.AsMemory(0, input.Length), cancellationToken);
                await csEncrypt.FlushAsync(cancellationToken);
                csEncrypt.FlushFinalBlock();
            }

            string base64 = Convert.ToBase64String(msEncrypt.ToArray());
            return ToBase64Url(base64);
        }

        // --- helpers ---
        private static byte[] Base64UrlDecodeToBytes(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            // pad with '=' to multiple of 4
            int pad = (4 - (s.Length % 4)) % 4;
            if (pad > 0)
            {
                s += new string('=', pad);
            }

            return Convert.FromBase64String(s);
        }

        private static string ToBase64Url(string base64)
        {
            return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        private static byte[] Parse(string val, string format)
        {
            var f = format.ToLowerInvariant().Trim();
            if (f == "raw")
            {
                return Encoding.UTF8.GetBytes(val);
            }

            if (f == "base64")
            {
                return Convert.FromBase64String(val.Trim());
            }

            if (f == "hex")
            {
                return HexToBytes(val);
            }

            throw new ArgumentException("Unknown key/IV format.");
        }

        private static byte[] HexToBytes(string hex)
        {
            hex = hex.Trim();
            if (hex.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                hex = hex[2..];
            }

            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex length must be even.");
            }

            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }

            return bytes;
        }
    }
}
