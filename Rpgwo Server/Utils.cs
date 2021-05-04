using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rpgwo_Server
{
    public class Utils
    {
        public static byte CalcCheckSum(byte[] data)
        {
            bool sw = false;
            Int16 num1 = 0;
            Int16 num2 = 0;

            for (int i = 0; i < data.Length; i++)
            {
                char value = Convert.ToString(data[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            return (byte)((num1 + (num2 * 3)) % 255);
        }

        public static byte CalcCheckSum(byte[] data, byte rnd)
        {
            bool sw = false;
            Int16 num1 = 0;
            Int16 num2 = 0;

            for (int i = 0; i < data.Length; i++)
            {
                char value = Convert.ToString(data[i]).ElementAt<char>(0);

                if (sw == false)
                {
                    num1 += Convert.ToByte(value);
                    sw = true;
                }
                else
                {
                    num2 += Convert.ToByte(value);
                    sw = false;
                }
            }

            return (byte)((num1 + rnd + (num2 * 3)) % 255);
        }
    }
}
