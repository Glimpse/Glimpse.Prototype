using System.Security.Cryptography;
using System.Text;

namespace Glimpse
{
    public static class Md5Extensions
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
    }
}