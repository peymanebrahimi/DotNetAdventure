using System;
using System.IO;
using System.Security.Cryptography;

namespace CryptoConsoleApp1.Symmetric
{
    class SymmetricEncryption1
    {

        public static void Encrypt(byte[] key, byte[] iv)
        {
            byte[] data = { 1, 2, 3, 4, 5, 6 };   // This is what we're encrypting.

            using SymmetricAlgorithm algorithm = Aes.Create();
            using ICryptoTransform cryptoTransform = algorithm.CreateEncryptor(key, iv);

            using Stream fileStream = File.Create("encrypted.bin");
            using Stream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
        }

        public static void Decrypt(byte[] key, byte[] iv)
        {
            using SymmetricAlgorithm algorithm = Aes.Create();
            using ICryptoTransform cryptoTransform = algorithm.CreateDecryptor(key, iv);

            using Stream fileStream = File.OpenRead("encrypted.bin");
            using Stream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Read);

            for (int b; (b = cryptoStream.ReadByte()) > -1;)
                Console.Write(b + " ");
        }
    }
}
