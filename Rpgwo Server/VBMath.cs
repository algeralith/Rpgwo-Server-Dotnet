using System;
using System.Collections.Generic;
using System.Text;

namespace Rpgwo_Server
{
	public sealed class VBMath
	{
		private static int _seed = 0;

		/// <summary>Returns a random number of type Single.</summary>
		/// <returns>The next random number in the sequence.</returns>
		/// <filterpriority>1</filterpriority>
		public static float Rnd()
		{
			return Rnd(1f);
		}

		/// <summary>Returns a random number of type Single.</summary>
		/// <returns>If number is less than zero, Rnd generates the same number every time, using <paramref name="Number" /> as the seed. If number is greater than zero, Rnd generates the next random number in the sequence. If number is equal to zero, Rnd generates the most recently generated number. If number is not supplied, Rnd generates the next random number in the sequence.</returns>
		/// <param name="Number">Optional. A Single value or any valid Single expression.</param>
		/// <filterpriority>1</filterpriority>
		public static float Rnd(float Number)
		{
			int num = _seed;
			checked
			{
				if ((double)Number != 0.0)
				{
					if ((double)Number < 0.0)
					{
						num = BitConverter.ToInt32(BitConverter.GetBytes(Number), 0);
						long num2 = num & uint.MaxValue;
						num = (int)((num2 + (num2 >> 24)) & 0xFFFFFF);
					}
					num = (int)((unchecked((long)num) * 1140671485L + 12820163) & 0xFFFFFF);
				}
				_seed = num;
			}
			return (float)num / 16777216f;
		}

		/// <summary>Initializes the random-number generator.</summary>
		/// <filterpriority>1</filterpriority>
		public static void Randomize()
		{
			float timer = GetTimer();
			int rndSeed = _seed;
			int num = BitConverter.ToInt32(BitConverter.GetBytes(timer), 0);
			num = ((num & 0xFFFF) ^ (num >> 16)) << 8;
			rndSeed = (_seed = ((rndSeed & -16776961) | num));
		}

		/// <summary>Initializes the random-number generator.</summary>
		/// <param name="Number">Optional. An Object or any valid numeric expression.</param>
		/// <filterpriority>1</filterpriority>
		public static void Randomize(double Number)
		{
			int rndSeed = _seed;
			int num = (!BitConverter.IsLittleEndian) ? BitConverter.ToInt32(BitConverter.GetBytes(Number), 0) : BitConverter.ToInt32(BitConverter.GetBytes(Number), 4);
			num = ((num & 0xFFFF) ^ (num >> 16)) << 8;
			rndSeed = (_seed = ((rndSeed & -16776961) | num));
		}

		private static float GetTimer()
		{
			DateTime now = DateTime.Now;
			return (float)((double)checked((60 * now.Hour + now.Minute) * 60 + now.Second) + (double)now.Millisecond / 1000.0);
		}
	}
}
