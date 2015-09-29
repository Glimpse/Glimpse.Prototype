using System.Security.Cryptography;
using System.Text;

namespace Glimpse
{
    public static class HashExtensions
    {
        private static readonly MD5 _md5 = MD5.Create();

        public static string Md5(this string utf8Input)
        {
            var bytes = Encoding.UTF8.GetBytes(utf8Input);
            var hash = _md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        // Initial and Polynomial parameters as defined by OpenPGP spec/usage: http://reveng.sourceforge.net/crc-catalogue/17plus.htm
        private const int _init = 0x0b704ce;
        private const int _poly = 0x1864cfb;

        public static string Crc24(this string input)
        {
            return Crc24(input, Encoding.UTF8);
        }

        public static string Crc24(this string input, Encoding encoding)
        {
            return Crc24(encoding.GetBytes(input));
        }

        public static string Crc24(this byte[] input)
        {
            int crc = _init;

            for (int i = 0; i < input.Length; i++)
            {
                crc ^= input[i] << 16;

                for (int j = 0; j < 8; j++)
                {
                    crc <<= 1;
                    if ((crc & 0x1000000) != 0)
                    {
                        crc ^= _poly;
                    }
                }
            }

            return crc.ToString("X");
        }
    }
}