using System;
using System.Numerics;
using System.Text;

namespace CSKripto2_2
{
    class Program
    {
        private static BigInteger gGeneratorValue = 3;
        public static BigInteger nPrimeNumber = 997;
        private static string pass = "sziauram123";

        private static Random rnd = new Random();
        private static BigInteger v = rnd.Next(1, (int)nPrimeNumber); // Kliens random száma
        private static BigInteger c = rnd.Next(1, (int)nPrimeNumber); // Szerver random száma

        static void Main(string[] args)
        {
            Console.WriteLine("Generation value: " + gGeneratorValue);
            Console.WriteLine("Prime number: " + nPrimeNumber);
            Console.WriteLine("Password: " + pass);
            Console.WriteLine();

            BigInteger x = BitConverter.ToInt32(Encoding.ASCII.GetBytes(CreateMD5(pass)), 0) % nPrimeNumber;
            Console.WriteLine("x: " + x);
            BigInteger y = (BigInteger.Pow(gGeneratorValue,(int)x) % nPrimeNumber);
            Console.WriteLine("y: " + y);

            Console.WriteLine("-------- LOGIN start --------");

            
            Console.WriteLine("v: " + v);
            BigInteger t = BigInteger.Pow(gGeneratorValue, (int)v) % nPrimeNumber;
            Console.WriteLine("t: " + t);

            BigInteger r = v - (c * x);
            Console.WriteLine("r: " + r);

            BigInteger Result;
            Console.WriteLine("ModInverse: " + modInverse((BigInteger.Pow(3, -(int)r) % nPrimeNumber), nPrimeNumber));

            if (r < 0)
                Result = (modInverse((BigInteger.Pow(gGeneratorValue, -(int)r) % nPrimeNumber), nPrimeNumber) * (BigInteger.Pow(y, (int)c) % nPrimeNumber)) % nPrimeNumber;
            else
                Result = ((BigInteger.Pow(gGeneratorValue, (int)r) % nPrimeNumber) * (BigInteger.Pow(y, (int)c) % nPrimeNumber)) % nPrimeNumber;

            Console.WriteLine("Result: " + Result);

            if (t == Result)
                Console.WriteLine("Sikeres belépés");
            else
                Console.WriteLine("Sikertelen belépés.");
        }

        private static int modInverse(BigInteger a, BigInteger m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return -1;
        }

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }


    }
}
