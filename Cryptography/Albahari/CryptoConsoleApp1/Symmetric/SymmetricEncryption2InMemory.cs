using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoConsoleApp1.Symmetric
{
    class SymmetricEncryption2InMemory
    {
        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using Aes algorithm = Aes.Create();
            using ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv);
            return Crypt(data, encryptor);
        }

        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using Aes algorithm = Aes.Create();
            using ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv);
            return Crypt(data, decryptor);
        }

        static byte[] Crypt(byte[] data, ICryptoTransform cryptor)
        {
            MemoryStream m = new MemoryStream();
            // Here, CryptoStreamMode.Write works best for both encryption and decryption,
            // since in both cases we’re “pushing” into a fresh memory stream.
            using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return m.ToArray();
        }

        // string overloads

        public static string Encrypt(string data, byte[] key, byte[] iv)
        {
            return Convert.ToBase64String(
                Encrypt(Encoding.UTF8.GetBytes(data), key, iv));
        }

        public static string Decrypt(string data, byte[] key, byte[] iv)
        {
            return Encoding.UTF8.GetString(
                Decrypt(Convert.FromBase64String(data), key, iv));
        }

        // Chaining Encryption Streams
        public static async Task ChainingEncryptionStreams()
        {
            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            var cryptoRng = RandomNumberGenerator.Create();
            cryptoRng.GetBytes(key);
            cryptoRng.GetBytes(iv);

            using Aes algorithm = Aes.Create();
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            using (Stream f = File.Create("serious.bin"))
            using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
            using (Stream d = new DeflateStream(c, CompressionMode.Compress))
            using (StreamWriter w = new StreamWriter(d))
                await w.WriteLineAsync("Small and secure!");

            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            using (Stream f = File.OpenRead("serious.bin"))
            using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
            using (Stream d = new DeflateStream(c, CompressionMode.Decompress))
            using (StreamReader r = new StreamReader(d))
                Console.WriteLine(await r.ReadLineAsync());     // Small and secure!
        }

    }
}