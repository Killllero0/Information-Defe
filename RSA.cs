﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Защита_Информаций
{
    internal class RSA
    {
        private Random rng = new Random(DateTime.Now.Millisecond);
        public BigInteger e {  get; set; }
        public BigInteger q { get; set; }
        public BigInteger fN { get; set; }

        public BigInteger d { get; set; }

        public BigInteger RSA_1()
        {
            throw new NotImplementedException();
        }

        public RSA()
        {
            BigInteger testStringBigInteger = new BigInteger(2122353412);
            BigInteger p = GetPrimeNumber();
            BigInteger q = GetPrimeNumber();
            while(p == q)
            {
                q = GetPrimeNumber();
            }

            BigInteger N = p * q;
            BigInteger fN = EulerFunction(p, q);

            //BigInteger e = GetE(fN);
            BigInteger e = 65537;

            BigInteger d = ExtendedEuclideanAlgorithm(fN, e);

            BigInteger EncodeText = BigInteger.ModPow(testStringBigInteger, e, N);
            BigInteger DecodeText = BigInteger.ModPow(EncodeText, d, N);

        }

        /// <summary>
        /// Метод получения простых чисел
        /// </summary>
        /// <returns>Простое число</returns>
        private BigInteger GetPrimeNumber()
        {
            bool isPrime = false;
            BigInteger number = 0;
            while (!isPrime)
            {
                number = rng.Next(100000, 2000000000);
                if (MillerRabinTest(number, 10))
                {
                    isPrime = true;
                }
            }
            return number;
        }

        /// <summary>
        /// Метод проверящий число на простоту
        /// </summary>
        /// <param name="n">Число</param>
        /// <param name="k">Число раундом</param>
        /// <returns>true если это число вероятно простое, false в противном случае</returns>
        private bool MillerRabinTest(BigInteger n, int k)
        {
            // если n == 2 или n == 3 - эти числа простые, возвращаем true
            if (n == 2 || n == 3)
                return true;

            // если n < 2 или n четное - возвращаем false
            if (n < 2 || n % 2 == 0)
                return false;

            // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
            BigInteger t = n - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            // повторить k раз
            for (int i = 0; i < k; i++)
            {
                // выберем случайное целое число a в отрезке [2, n − 2]
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                byte[] _a = new byte[n.ToByteArray().LongLength];

                BigInteger a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= n - 2);

                // x ← a^t mod n, вычислим с помощью возведения в степень по модулю
                BigInteger x = BigInteger.ModPow(a, t, n);

                // если x == 1 или x == n − 1, то перейти на следующую итерацию цикла
                if (x == 1 || x == n - 1)
                    continue;

                // повторить s − 1 раз
                for (int r = 1; r < s; r++)
                {
                    // x ← x^2 mod n
                    x = BigInteger.ModPow(x, 2, n);

                    // если x == 1, то вернуть "составное"
                    if (x == 1)
                        return false;

                    // если x == n − 1, то перейти на следующую итерацию внешнего цикла
                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }

            // вернуть "вероятно простое"
            return true;
        }

        private BigInteger EulerFunction(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        private BigInteger GetE(BigInteger fN)
        {
            bool isPrime = false;
            BigInteger number = 0;
            while (!isPrime)
            {
                number = rng.Next(100000, 2000000000);
                if (MillerRabinTest(number, 10))
                {
                    isPrime = true;
                }
            }
            return number;
        }


        private BigInteger ExtendedEuclideanAlgorithm(BigInteger fN, BigInteger e)
        {
            BigInteger a = fN;
            BigInteger b = e;

            BigInteger q, r = 0;
            BigInteger x1 = 0, x2 = 1, y1 = 1, y2 = 0;
            BigInteger x, y;
            
            while(b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }
            return fN - BigInteger.Abs(BigInteger.Min(x2, y2));
        }
    }
}
