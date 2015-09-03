using System.Text;

namespace Glimpse
{
    // Crc16 implementaion from Marc Gravell at http://stackoverflow.com/a/22861111/107289
    public static class Crc16Extensions
    {
        const ushort Polynomial = 0xA001;
        static readonly ushort[] Table = new ushort[256];

        public static string Crc16(this string utf8Input)
        {
            var bytes = Encoding.UTF8.GetBytes(utf8Input);
            return ComputeChecksum(bytes).ToString("x2");
        }

        public static ushort ComputeChecksum(byte[] bytes)
        {
            ushort crc = 0;
            for (var i = 0; i < bytes.Length; ++i)
            {
                byte index = (byte)(crc ^ i);
                crc = (ushort)((crc >> 8) ^ Table[index]);
            }
            return crc;
        }

        static Crc16Extensions()
        {
            for (ushort i = 0; i < Table.Length; ++i)
            {
                ushort value = 0;
                var temp = i;
                for (byte j = 0; j < 8; ++j)
                {
                    if (((value ^ temp) & 0x0001) != 0)
                    {
                        value = (ushort)((value >> 1) ^ Polynomial);
                    }
                    else
                    {
                        value >>= 1;
                    }
                    temp >>= 1;
                }
                Table[i] = value;
            }
        }
    }
}