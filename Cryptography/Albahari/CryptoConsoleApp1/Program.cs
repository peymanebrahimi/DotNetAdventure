using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CryptoConsoleApp1.Symmetric;

namespace CryptoConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var key = new byte[32];
            var iv = new byte[16];
            RandomNumberGenerator rand = RandomNumberGenerator.Create();
            rand.GetBytes(key);
            rand.GetBytes(iv);

            //RunEx1(key, iv);

            //RunEx2(key, iv);

            await RunEx3();

            Console.ReadLine();
        }



        private static void RunEx1(byte[] key, byte[] iv)
        {
            SymmetricEncryption1.Encrypt(key, iv);
            SymmetricEncryption1.Decrypt(key, iv);
        }

        private static void RunEx2(byte[] key, byte[] iv)
        {
            string encrypted = SymmetricEncryption2InMemory.Encrypt("Yeah!", key, iv);
            Console.WriteLine(encrypted);

            string decrypted = SymmetricEncryption2InMemory.Decrypt(encrypted, key, iv);
            Console.WriteLine(decrypted);
        }

        private static async Task RunEx3()
        {
            await SymmetricEncryption2InMemory.ChainingEncryptionStreams();
        }
    }
}
